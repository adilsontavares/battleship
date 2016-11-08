using Tween.Animation.Callbacks;
using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation
{
    public class AnimationBehaviour : MonoBehaviour
    {
        private float _time;
        private bool _animating;
        private bool _paused;
        private float _elapsedTime;
        private float _elapsedDelay;
        private float _duration;
        private float _delay;
        private bool _inverse;

        public object Data { get; set; }

        public bool Completed { get; private set; }

        public string Name { get; set; }

        public float Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public float Delay
        {
            get { return _delay; }
            set
            {
                if (!_animating) _delay = value;
                else Debug.LogWarning("Animation delay can only be changed before the animation starts.");
            }
        }

        public bool Inverse
        {
            get { return _inverse; }
            set
            {
                if (!_animating) _inverse = value;
                else Debug.LogWarning("Animation inverse can only be changed before the animation starts.");
            }
        }

        public bool ConsiderTimeScale { get; set; }
        public IAnimationEase Ease { get; set; }
        public FinishCallback FinishCallback { get; set; }
        public StartCallback StartCallback { get; set; }
        public UpdateCallback UpdateCallback { get; set; }
        public DoneCallback DoneCallback { get; set; }
        public CancelCallback CancelCallback { get; set; }

        private void Awake()
        {
            _duration = 0f;

            Restart(true);
        }

        public void Start()
        {
            Resume();
        }

        /// <summary>
        /// Reinicia a animacao. Por padrao, esta sera pausada ao reiniciar.
        /// </summary>
        public void Restart()
        {
            Restart(true);
        }

        /// <summary>
        /// Reinicia a animacao.
        /// </summary>
        /// <param name="pause">Se pause for true, a animacao e pausada ao reiniciar. Caso contrario, ela reinicia e ja inicia novamente</param>
        public void Restart(bool pause)
        {
            Restart(pause, true);
        }

        public void Restart(bool pause, bool resetDelay)
        {
            _time = 0f;
            _elapsedTime = Time.realtimeSinceStartup;

            Completed = false;

            if (resetDelay)
            {
                _animating = false;
                _elapsedDelay = 0f;
            }

            if (pause)
                Pause();
            else if (Mathf.Approximately(_duration, 0f))
                CompleteAnimationNow();
        }

        private void CompleteAnimationNow()
        {
            if (UpdateCallback != null)
                UpdateCallback(this, Inverse ? 0f : 1f);

            if (DoneCallback != null)
                DoneCallback(this);

            Completed = true;
            EndAnimation();
        }

        public void Resume()
        {
            _paused = false;
            _elapsedTime = Time.realtimeSinceStartup;

            if (Mathf.Approximately(_duration, 0f))
                CompleteAnimationNow();
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Cancel()
        {
            _paused = true;

            // Chama o callback de cancelamento (se existir)
            if (CancelCallback != null)
                CancelCallback(this);

            EndAnimation();
        }

        private void LateUpdate()
        {
            // Se estiver pausado, nao atualiza a animacao
            if (_paused) return;

            // Atualiza time decorrido
            var realtimeSinceStartup = Time.realtimeSinceStartup;
            var deltaTime = (realtimeSinceStartup - _elapsedTime) * (ConsiderTimeScale ? Time.timeScale : 1f);
            _elapsedTime = realtimeSinceStartup;

            // Atualiza delay
            if (!_animating)
            {
                _elapsedDelay += deltaTime;
                if (_elapsedDelay >= _delay)
                {
                    _animating = true;

                    if (StartCallback != null)
                        StartCallback(this);
                }
            }

            if (_animating)
            {
                // Atualiza time da animacao
                _time += deltaTime / Duration;

                if (_time >= 1f)
                {
                    Completed = true;
                    _time = 1f;
                }

                // Chama atualizacao
                if (UpdateCallback != null)
                {
                    var time = Ease.ConvertTime(_time);

                    if (Inverse)
                        time = 1f - time;

                    UpdateCallback(this, time);
                }

                // Chama encerramento da animacao se necessario
                if (Completed)
                {
                    // Chama o callback de finalizacao com sucesso (se existir)
                    if (DoneCallback != null)
                        DoneCallback(this);

                    EndAnimation();
                }
            }
        }

        private void EndAnimation()
        {
            // Chama o callback de finalizacao (se existir)
            if (FinishCallback != null)
                FinishCallback(this);

            Destroy(this);
        }
    }
}
