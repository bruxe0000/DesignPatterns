using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeDesignPatterns.BehavioralDP
{
    /// <summary>
    /// Pros:
    /// - Single Responsibility Principle.
    /// Organize the code related to particular states into separate classes.
    /// - Open/Closed Principle. 
    /// Introduce new states without changing existing state classes or the context.
    /// - Simplify the code of the context by eliminating bulky state machine conditionals.
    /// 
    /// Cons:
    /// - Applying the pattern can be overkill 
    /// if a state machine has only a few states or rarely changes.
    /// </summary>

    public class AudioPlayer
    {
        private State _state;
        private long _playlist = 10;
        private int _currentSong = 0;

        public bool IsPlaying { get; set; } = false;

        public bool IsDoubleClick { get; set; } = false;        
        
        // Other objects must be able to switch the audio player's
        // active state.
        public void ChangeState(State state)
        {
            _state = state;
        }

        public string GetState()
        {
            if (_state is LockedState)
            {
                return "Lock mode";
            }
            else if(_state is ReadyState)
            {
                return "Ready mode";
            }
            else if (_state is PlayingState)
            {
                return $"Playing mode";
            }
            return "Unknown";
        }

        public string GetStateMessage()
        {
            return _state.stateMessage;
        }

        // UI methods delegate execution to the active state.
        public void ClickLock()
        {
            _state.ClickLock();
        }

        public void ClickPlay()
        {
            _state.ClickPlay();
        }

        public void ClickNext()
        {
            _state.ClickNext();
        }

        public void ClickPrevious()
        {
            _state.ClickPrevious();
        }

        // A state may call some service methods on the context.
        public void StartPlayback()
        {
            IsPlaying = true;
            _state.stateMessage = $"Start playing song {_currentSong}";
        }

        public void StopPlayback()
        {
            IsPlaying = false;
            _state.stateMessage = $"Stop playing song {_currentSong}";
        }

        public void NextSong()
        {
            _currentSong = _currentSong + 1 > _playlist ? _currentSong : _currentSong + 1;
            _state.stateMessage = $"Playing song {_currentSong}";
        }
        public void PreviousSong()
        {
            _currentSong = _currentSong - 1 < 0 ? _currentSong : _currentSong - 1;
            _state.stateMessage = $"Playing song {_currentSong}";
        }

        public void FastForward(int time)
        {
            _state.stateMessage = $"Forward by {time} second";
        }

        public void Rewind(int time)
        {
            _state.stateMessage = $"Rewind by {time} second";
        }
    }

    public abstract class State
    {
        protected AudioPlayer player;
        public string stateMessage = string.Empty;

        public State(AudioPlayer player)
        {
            this.player = player;
        }

        public abstract void ClickLock();
        public abstract void ClickPlay();
        public abstract void ClickNext();
        public abstract void ClickPrevious();
    }

    public class LockedState : State
    {
        public LockedState(AudioPlayer player) : base(player)
        {
            this.player = player;
            stateMessage = "Change to \"locked\"";
        }

        /// <summary>
        /// When you unlock a locked player, it may assume one of two states.
        /// </summary>
        public override void ClickLock()
        {
            if (player.IsPlaying)
            {
                player.ChangeState(new PlayingState(player));
                stateMessage = string.Empty;
            }
            else
            {
                player.ChangeState(new ReadyState(player));
                stateMessage = string.Empty;
            }
        }

        public override void ClickPlay()
        {
            stateMessage = "Locked, so do nothing.";
        }

        public override void ClickNext()
        {
            stateMessage = "Locked, so do nothing.";
        }

        public override void ClickPrevious()
        {
            stateMessage = "Locked, so do nothing.";
        }
    }

    // They can also trigger state transitions in the context.
    public class ReadyState : State
    {
        public ReadyState(AudioPlayer player) : base(player)
        {
            this.player = player;
            stateMessage = "Change to \"ready\"";
        }

        public override void ClickLock()
        {
            player.ChangeState(new LockedState(player));
        }

        public override void ClickPlay()
        {
            player.ChangeState(new PlayingState(player));
            player.StartPlayback();
        }

        public override void ClickNext()
        {
            player.NextSong();
        }

        public override void ClickPrevious()
        {
            player.PreviousSong();
        }
    }

    public class PlayingState : State
    {
        public PlayingState(AudioPlayer player) : base(player)
        {
            this.player = player;
            stateMessage = "Change to \"play\"";
        }

        public override void ClickLock()
        {
            player.ChangeState(new LockedState(player));
        }

        public override void ClickPlay()
        {
            player.ChangeState(new ReadyState(player));
            player.StopPlayback();
        }

        public override void ClickNext()
        {
            if (player.IsDoubleClick)
            {
                player.FastForward(5);
            }
            else
            {
                player.NextSong();
            }
        }

        public override void ClickPrevious()
        {
            if (player.IsDoubleClick)
            {
                player.Rewind(5);
            }
            else
            {
                player.PreviousSong();
            }
        }
    }

    public class StateExample
    {
        public static void PlayerRun()
        {
            AudioPlayer player = new AudioPlayer();
            player.ChangeState(new ReadyState(player));
            
            int option;
            bool isExit = false;
            while (!isExit)
            {
                Console.WriteLine($"State Message: {player.GetStateMessage()}");
                Console.WriteLine($"Current Mode: {player.GetState()}");

                Console.WriteLine("Choose the option:");
                Console.WriteLine("1. Play/Stop");
                Console.WriteLine("2. Lock");
                Console.WriteLine("3. Next");
                Console.WriteLine("4. Previous");
                Console.WriteLine("5. Doubleclick - Next");
                Console.WriteLine("6. Doubleclick - Previous");
                Console.WriteLine("0. Exit");

                int.TryParse(Console.ReadLine(), out option);

                switch (option)
                {
                    case 0:
                        isExit = true;
                        break;
                    case 1:
                        player.ClickPlay();
                        break;
                    case 2:
                        player.ClickLock();
                        break;
                    case 3:
                        player.IsDoubleClick = false;
                        player.ClickNext();
                        break;
                    case 4:
                        player.IsDoubleClick = false;
                        player.ClickPrevious();
                        break;
                    case 5:
                        player.IsDoubleClick = true;
                        player.ClickNext();
                        break;
                    case 6:
                        player.IsDoubleClick = true;
                        player.ClickNext();
                        break;
                }

                Console.Clear();
            }

            Console.WriteLine("Program ended.");
        }
    }
}
