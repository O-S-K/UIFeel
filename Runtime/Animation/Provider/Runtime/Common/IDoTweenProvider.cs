using DG.Tweening;

namespace OSK
{
    public interface IDoTweenProvider
    {
        void InitFromMG(bool playOnEnable, bool setAutoKill, UpdateType updateType, bool useUnscaledTime);
        Tweener Tweener { get; }
        bool IsPlaying { get; }
        void ProgressTween(bool isPlayBackwards);
        void Play();
        void Rewind();
        void Stop();
        float GetDuration();
        void Preview(float time);
    }
}