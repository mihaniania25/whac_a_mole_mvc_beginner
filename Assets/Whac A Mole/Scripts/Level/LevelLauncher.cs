using UnityEngine;
using MeShineFactory.WhacAMole.Level.Controller;
using MeShineFactory.WhacAMole.Level.Model;
using MeShineFactory.WhacAMole.Level.View;

namespace MeShineFactory.WhacAMole.Level
{
    public class LevelLauncher : MonoBehaviour
    {
        [SerializeField] private LevelView view;

        private ILevelModel model;
        private ILevelController controller;

        private void Awake()
        {
            model = GetLevelModel();
            model.Setup();

            view.Setup(model);

            controller = GetLevelController();
            controller.Setup(view, model);
        }

        private ILevelModel GetLevelModel()
        {
            return new LevelModel();
        }

        private ILevelController GetLevelController()
        {
            return new LevelController();
        }

        private void OnDestroy()
        {
            view.Dispose();
            controller.Dispose();
            model.Dispose();
        }
    }
}
