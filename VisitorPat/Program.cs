using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace visitors
{

    public interface ICommandVisitor
    {
        void Visit(StartCommand startCommand);
        void Visit(StopCommand stopCommand);
        void Visit(GetStatusCommand getStatusCommand);
        void Visit(GetTargetsCommand getTargetsCommand);
    }

    public abstract class Command
    {
        public abstract void Accept(ICommandVisitor messageVisitor);
    }

    public class GetTargetsCommand : Command
    {
        public override void Accept(ICommandVisitor messageVisitor)
        {
            messageVisitor.Visit(this);
        }
    }

    public class GetStatusCommand : Command
    {
        public override void Accept(ICommandVisitor messageVisitor)
        {
            messageVisitor.Visit(this);
        }
    }

    public class StopCommand : Command
    {
        public override void Accept(ICommandVisitor messageVisitor)
        {
            messageVisitor.Visit(this);
        }
    }

    public class StartCommand : Command
    {
        public override void Accept(ICommandVisitor messageVisitor)
        {
            messageVisitor.Visit(this);
        }
    }

    /*
     * CLASSIC
     */
    public class CommandVisitorClassic : ICommandVisitor
    {
        public void VisitAll(IEnumerable<Command> commands)
        {
            foreach (var command in commands)
            {
                command.Accept(this);
            }
        }

        public void Visit(StartCommand startCommand)
        {
            Console.WriteLine(startCommand.GetType());
        }

        public void Visit(StopCommand stopCommand)
        {
            Console.WriteLine(stopCommand.GetType());
        }

        public void Visit(GetStatusCommand getStatusCommand)
        {
            Console.WriteLine(getStatusCommand.GetType());
        }

        public void Visit(GetTargetsCommand getTargetsCommand)
        {
            Console.WriteLine(getTargetsCommand.GetType());
        }
    }

    /*
     * DYNAMIC
     */
    public class DynamicCommandVisitor
    {
        public void VisitAll(IEnumerable<Command> commands)
        {
            foreach (var command in commands)
            {
                // cast as dynamic
                dynamic dCommand = command;
                Visit(dCommand);
            }
        }

        public void Visit(StartCommand startCommand)
        {
            Console.WriteLine(startCommand.GetType());
        }

        public void Visit(StopCommand stopCommand)
        {
            Console.WriteLine(stopCommand.GetType());
        }

        public void Visit(GetStatusCommand getStatusCommand)
        {
            Console.WriteLine(getStatusCommand.GetType());
        }

        public void Visit(GetTargetsCommand getTargetsCommand)
        {
            Console.WriteLine(getTargetsCommand.GetType());
        }

    }

    /*
     * DELEGATE
     */
    class DelegateCommandVisitor
    {
        public DelegateCommandVisitor()
        {
            _methods = new Dictionary<Type, Action<Command>>
            {
                { typeof(StartCommand), a => Visit((StartCommand) a) },
                { typeof(StopCommand), a => Visit((StopCommand) a) },
                { typeof(GetStatusCommand), a => Visit((GetStatusCommand) a) },
                { typeof(GetTargetsCommand), a => Visit((GetTargetsCommand) a) },
            };
        }

        public void VisitAll(IEnumerable<Command> commands)
        {
            foreach (var command in commands)
                _methods[command.GetType()](command);
        }

        public void Visit(StartCommand startCommand)
        {
            Console.WriteLine(startCommand.GetType());
        }

        public void Visit(StopCommand stopCommand)
        {
            Console.WriteLine(stopCommand.GetType());
        }

        public void Visit(GetStatusCommand getStatusCommand)
        {
            Console.WriteLine(getStatusCommand.GetType());
        }

        public void Visit(GetTargetsCommand getTargetsCommand)
        {
            Console.WriteLine(getTargetsCommand.GetType());
        }

        readonly Dictionary<Type, Action<Command>> _methods;
    }


    class Program
    {
        private static void Main(string[] args)
        {
            // set up input collections
            var messages = new List<Command>
                {
                    new GetStatusCommand(),
                    new GetTargetsCommand(),
                    new StartCommand(),
                    new StopCommand()
                };
            var commandVisitor = new CommandVisitorClassic();
            var dynamicVisitor = new DynamicCommandVisitor();
            var delegateVisitor = new DelegateCommandVisitor();

            commandVisitor.VisitAll(messages);
            Console.WriteLine();

            dynamicVisitor.VisitAll(messages);
            Console.WriteLine();

            delegateVisitor.VisitAll(messages);
            Console.ReadKey();
        }
    }
}
