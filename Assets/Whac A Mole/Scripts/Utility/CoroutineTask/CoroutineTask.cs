using UnityEngine;
using System.Collections;

namespace MeShineFactory.WhacAMole.Utility
{
	public class CoroutineTask
	{
		private Coroutine coroutine;
		private static CoroutineRunner coroutineRunner;

		static CoroutineTask()
		{
            GameObject runnerGO = new GameObject("Coroutine Runner");
            coroutineRunner = runnerGO.AddComponent<CoroutineRunner>();

            GameObject.DontDestroyOnLoad(coroutineRunner);
        }

		public CoroutineTask(IEnumerator c)
		{
			coroutine = coroutineRunner.StartCoroutine(c);
		}

		public void Stop()
		{
			if (coroutine != null && coroutineRunner != null && coroutineRunner.gameObject != null)
				coroutineRunner.StopCoroutine(coroutine);
		}
	}
}