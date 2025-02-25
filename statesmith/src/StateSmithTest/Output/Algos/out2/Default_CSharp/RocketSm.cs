// Autogenerated with StateSmith.
// Algorithm: Balanced2. See https://github.com/StateSmith/StateSmith/wiki/Algorithms

#nullable enable

// Generated state machine
public partial class RocketSm
{
    public enum EventId
    {
        EV1 = 0,
        EV2 = 1,
    }

    public const int EventIdCount = 2;

    public enum StateId
    {
        ROOT = 0,
        GROUP = 1,
        G1 = 2,
        G2 = 3,
        S1 = 4,
    }

    public const int StateIdCount = 5;

    // Used internally by state machine. Feel free to inspect, but don't modify.
    public StateId stateId;

    // State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
    public RocketSm()
    {
    }

    // Starts the state machine. Must be called before dispatching events. Not thread safe.
    public void Start()
    {
        ROOT_enter();
        // ROOT behavior
        // uml: TransitionTo(ROOT.<InitialState>)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `ROOT.<InitialState>`.
            // ROOT.<InitialState> is a pseudo state and cannot have an `enter` trigger.

            // ROOT.<InitialState> behavior
            // uml: TransitionTo(group)
            {
                // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

                // Step 2: Transition action: ``.

                // Step 3: Enter/move towards transition target `group`.
                GROUP_enter();

                // group.<InitialState> behavior
                // uml: TransitionTo(g1)
                {
                    // Step 1: Exit states until we reach `group` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

                    // Step 2: Transition action: ``.

                    // Step 3: Enter/move towards transition target `g1`.
                    G1_enter();

                    // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
                    return;
                } // end of behavior for group.<InitialState>
            } // end of behavior for ROOT.<InitialState>
        } // end of behavior for ROOT
    }

    // Dispatches an event to the state machine. Not thread safe.
    // Note! This function assumes that the `eventId` parameter is valid.
    public void DispatchEvent(EventId eventId)
    {
        switch (this.stateId)
        {
            // STATE: RocketSm
            case StateId.ROOT:
                switch (eventId)
                {
                    // Events not handled by this state:
                    case EventId.EV1: break;
                    case EventId.EV2: break;
                }
                break;

            // STATE: group
            case StateId.GROUP:
                switch (eventId)
                {
                    case EventId.EV1: GROUP_ev1(); break;
                    // Events not handled by this state:
                    case EventId.EV2: break;
                }
                break;

            // STATE: g1
            case StateId.G1:
                switch (eventId)
                {
                    case EventId.EV1: G1_ev1(); break;
                    // Events not handled by this state:
                    case EventId.EV2: break;
                }
                break;

            // STATE: g2
            case StateId.G2:
                switch (eventId)
                {
                    case EventId.EV2: G2_ev2(); break;
                    // Events not handled by this state:
                    case EventId.EV1: GROUP_ev1(); break; // First ancestor handler for this event
                }
                break;

            // STATE: s1
            case StateId.S1:
                switch (eventId)
                {
                    // Events not handled by this state:
                    case EventId.EV1: break;
                    case EventId.EV2: break;
                }
                break;
        }

    }

    // This function is used when StateSmith doesn't know what the active leaf state is at
    // compile time due to sub states or when multiple states need to be exited.
    private void ExitUpToStateHandler(StateId desiredState)
    {
        while (this.stateId != desiredState)
        {
            switch (this.stateId)
            {
                case StateId.GROUP: GROUP_exit(); break;

                case StateId.G1: G1_exit(); break;

                case StateId.G2: G2_exit(); break;

                case StateId.S1: S1_exit(); break;

                default: return;  // Just to be safe. Prevents infinite loop if state ID memory is somehow corrupted.
            }
        }
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state ROOT
    ////////////////////////////////////////////////////////////////////////////////

    private void ROOT_enter()
    {
        this.stateId = StateId.ROOT;
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state GROUP
    ////////////////////////////////////////////////////////////////////////////////

    private void GROUP_enter()
    {
        this.stateId = StateId.GROUP;
    }

    private void GROUP_exit()
    {
        this.stateId = StateId.ROOT;
    }

    private void GROUP_ev1()
    {
        // group behavior
        // uml: EV1 TransitionTo(s1)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(StateId.ROOT);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `s1`.
            S1_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            return;
        } // end of behavior for group

        // No ancestor handles this event.
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state G1
    ////////////////////////////////////////////////////////////////////////////////

    private void G1_enter()
    {
        this.stateId = StateId.G1;
    }

    private void G1_exit()
    {
        this.stateId = StateId.GROUP;
    }

    private void G1_ev1()
    {
        bool consume_event = false;

        // g1 behavior
        // uml: EV1 [a > 20] TransitionTo(g2)
        if (a > 20)
        {
            // Step 1: Exit states until we reach `group` state (Least Common Ancestor for transition).
            G1_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `g2`.
            G2_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            return;
        } // end of behavior for g1

        // Check if event has been consumed before calling ancestor handler.
        if (!consume_event)
        {
            GROUP_ev1();
        }
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state G2
    ////////////////////////////////////////////////////////////////////////////////

    private void G2_enter()
    {
        this.stateId = StateId.G2;
    }

    private void G2_exit()
    {
        this.stateId = StateId.GROUP;
    }

    private void G2_ev2()
    {
        // g2 behavior
        // uml: EV2 TransitionTo(g1)
        {
            // Step 1: Exit states until we reach `group` state (Least Common Ancestor for transition).
            G2_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `g1`.
            G1_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            return;
        } // end of behavior for g2

        // No ancestor handles this event.
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state S1
    ////////////////////////////////////////////////////////////////////////////////

    private void S1_enter()
    {
        this.stateId = StateId.S1;
    }

    private void S1_exit()
    {
        this.stateId = StateId.ROOT;
    }

    // Thread safe.
    public static string StateIdToString(StateId id)
    {
        switch (id)
        {
            case StateId.ROOT: return "ROOT";
            case StateId.GROUP: return "GROUP";
            case StateId.G1: return "G1";
            case StateId.G2: return "G2";
            case StateId.S1: return "S1";
            default: return "?";
        }
    }

    // Thread safe.
    public static string EventIdToString(EventId id)
    {
        switch (id)
        {
            case EventId.EV1: return "EV1";
            case EventId.EV2: return "EV2";
            default: return "?";
        }
    }
}
