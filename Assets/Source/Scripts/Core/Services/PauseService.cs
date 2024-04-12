using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Core.Services
{
    public class PauseService : MonoBehaviour
    {
        private List<IPause> _pauses = new List<IPause>();
        
        public void AddPause(IPause pause)
        {
            _pauses.Add(pause);
        }
        
        public void RemovePause(IPause pause)
        {
            _pauses.Remove(pause);
        }
        
        public void Pause()
        {
            foreach (var pause in _pauses)
            {
                pause.Pause();
            }
        }
        
        public void Resume()
        {
            foreach (var pause in _pauses)
            {
                pause.Resume();
            }
        }
    }
    
    public interface IPause
    {
        public void Pause();
        public void Resume();
    }
}