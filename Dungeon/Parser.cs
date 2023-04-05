using System.Collections;
using System.Collections.Generic;
using System;

namespace DungeonGame
{
    public enum ParserState {Normal, Battle, Character}
    public interface IParserState
    {
        ParserState State { get; }
        void Enter(Parser parser);
        void Exit(Parser parser);
    }
    public class ParserNormalState : IParserState
    {
        public ParserState State { get { return ParserState.Normal; } }
        public ParserNormalState()
        {

        }
        public void Enter(Parser parser)
        {
        }
        public void Exit(Parser parser)
        {
        }
    }
    public class ParserBattleState : IParserState
    {
        //For battle encounters
        public ParserState State { get { return ParserState.Battle; } }
        public ParserBattleState()
        {
            
        }
        public void Enter(Parser parser)
        {
            //This is a command for when a different state is entered.
            Command[] commandArray = {new QuitCommand(), new ExitCommand() };

            parser.Push(new CommandWords(commandArray));
    }
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }

    public class ParserCharacterState : IParserState
    {
        public ParserState State { get { return ParserState.Character; } }
        public ParserCharacterState()
        {

        }
        public void Enter(Parser parser)
        {
            //New commands
            Command[] commandArray = { new QuitCommand(), new NameCommand(), new ExitCommand(), new GoCommand(), new HelpCommand(), new OpenCommand(), new PickupCommand(), new DropCommand(), new QuestCommand()};
            parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            //Returns to the normal state.
            parser.Pop();
        }
    }

    public class Parser
    {
        private Stack <CommandWords> commands;
        private IParserState _state;
        public IParserState State
        {
            get
            {
                return _state;
            }
            set
            {
                //Exits old state
                _state.Exit(this);

                //Enter value of new state
                _state = value;

                //Enters new state and puts commands in the parse
                _state.Enter(this);
            }
        }

        public Parser() : this(new CommandWords())
        {

        }

        public Parser(CommandWords newCommands)
        {
            //Commands are then moved to a stack
            commands = new Stack<CommandWords>();
            _state = new ParserNormalState();
            Push(newCommands);
            //Pushes new commands
            NotificationCenter.Instance.addObserver("PlayerWillEnterState", PlayerWillEnterState);
        }
        public void PlayerWillEnterState(Notification notification)
        {
            Player player = (Player)notification.Object;
            Dictionary<string, Object> userInfo = notification.userInfo;
            IParserState state = (IParserState)userInfo["state"];
            if(State.State == ParserState.Character)
            {
                player.currentRoom = GameWorld.Instance.Entrance;
            }
            State = state;

        }
        public void Push(CommandWords newCommands)
        {
            commands.Push(newCommands);
        }

        public void Pop()
        {
            commands.Pop();
        }

        public Command parseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' ');
            if (words.Length > 0)
            {
                command = commands.Peek().get(words[0]);
                if (command != null)
                {
                    if (words.Length > 1)
                    {
                        command.secondWord = words[1];
                    }
                    else
                    {
                        command.secondWord = null;
                    }
                }
                else
                {
                    Console.WriteLine(">>>Did not find the command " + words[0]);
                }
            }
            else
            {
                Console.WriteLine("No words parsed!");
            }
            return command;
        }

        public string description()
        {
            return commands.Peek().description();
        }
    }
}
