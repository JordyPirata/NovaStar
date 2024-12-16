using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace Services
{
    public static class ThreadManager
    {
        public class Task
        {
            public string name;
            public int priority;
            public Action action;
            public Thread thread;
            public bool Enqueued { get{return queue.Contains(this);} }
            public bool Active { get{return active.Contains(this);} }
            public bool IsAlive { get{return queue.Contains(this) || active.Contains(this);} }

        }
        private static List<Task> queue = new();
        private static List<Task> active = new();
        public static int maxThreads = 3;
        public static int processorThreads = -1;
        public static bool autoMaxThreads = true;
        public static bool useMultithreading = true;

        static ThreadManager()
        {
            #if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= AbortOnPlaymodeChange;
            Application.wantsToQuit -= AbortOnExit;

            EditorApplication.playModeStateChanged += AbortOnPlaymodeChange;
            Application.wantsToQuit += AbortOnExit;
            #endif
        }
        public static Task Enqueue (Action action, int priority=0, string name=null)
		{
			Task task = new Task() { action=action, priority=priority, name=name };
			Enqueue(task);
			return task;
		}
        public static void Enqueue (Task task)
		{
			//if already executing - do nothing
			lock (active)
				if (active.Contains(task))
					return;

			lock (queue)
			{
				//if not in queue - enqueueing
				if (!queue.Contains(task))
					queue.Add(task);

				//if in queue  - do nothing
				else
					return;
			}

			LaunchThreads();
		}
        public static void Dequeue (Task task)
		{
			//if job is in queue and has not been started - just remove from queue
			lock (queue)
				if (queue.Contains(task))
				{
					queue.Remove(task);
					return;
				}
		}
        public static void LaunchThreads ()
		{
			lock (active)
				while (true) //active.Count < maxThreads && queue.Count != 0)
			{
				int curMaxThreads = maxThreads;
				if (autoMaxThreads) 
				{
					if (processorThreads < 0) processorThreads = SystemInfo.processorCount; //the first time LaunchThreads is called from main thread
					curMaxThreads = processorThreads-1;
				}

				if (active.Count >= curMaxThreads) break;

				Task task;
				lock (queue)
				{
					if (queue.Count == 0) break;

					int jobNum = GetMaxPriorityNum(queue);
					task = queue[jobNum]; 
					queue.RemoveAt(jobNum);
				}
		
				active.Add(task);

				if (useMultithreading)
				{
					Thread thread = new Thread(task.TaskThreadAction);
					lock (task)
						task.thread = thread;
					thread.Start();
				}
				else
					task.TaskThreadAction();
			}
		}
        public static void TaskThreadAction (this Task task)
		{
			try
				{ task.action(); }

			catch (ThreadAbortException) { }

			#if !MM_DEBUG
			catch (Exception e)
				{ Debug.LogError("Thread failed: " + e); }  //throw exception ignores VS
				//{ throw new Exception("Thread failed: " + e); }
			#endif

			finally
			{
				lock (active)
					active.Remove(task); //it SHOULD be in active

				//task.ended = true;
				LaunchThreads(); //updating threads once one done
			}
		}
        public static int GetMaxPriorityNum (List<Task> list)
		{
			int maxPriority = int.MinValue;
			int maxPriorityNum = -1;

			for (int i=list.Count-1; i>=0; i--) //for FIFO
			{
				int priority = list[i].priority;
				if (priority > maxPriority)
				{
					maxPriority = priority;
					maxPriorityNum = i;
				}
			}

//			Debug.Log("Selected " + maxPriority + " from list of " + list.Count + "\n" + list.ToStringMemberwise<Task>(o => o.priority.ToString()));

			return maxPriorityNum;
		}
        public static void Abort ()
		{
			//clearing queue (in that order so no job will pass from queue to active)
			lock (queue)
				queue.Clear();

			//clearing active
			List<Task> activeCopy;
			lock (active)
			{
				activeCopy = new List<Task>(active);
				active.Clear();
			}

			for (int i=0; i<activeCopy.Count; i++)
			{
				Task task = activeCopy[i];

				lock (task)
					if (task.thread != null) 
						task.thread.Abort(); 
			}
		}
        #if UNITY_EDITOR
		static void AbortOnPlaymodeChange (PlayModeStateChange state)
		{
			if (state==PlayModeStateChange.ExitingEditMode || state==PlayModeStateChange.ExitingPlayMode)
				Abort();
		}
		#endif

        static bool AbortOnExit ()
		{
			Abort(); return true;
		}
        public static int Count 
		{get{ 
			return queue.Count + active.Count; 
		}}

		public static bool IsWorking {get{ return queue.Count!=0 || active.Count!=0; }}
    }
}
