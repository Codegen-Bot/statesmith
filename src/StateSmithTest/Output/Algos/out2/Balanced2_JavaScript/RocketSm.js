// Autogenerated with StateSmith.
// Algorithm: Balanced2. See https://github.com/StateSmith/StateSmith/wiki/Algorithms

// Generated state machine
class RocketSm
{
    static EventId = 
    {
        EV1 : 0,
        EV2 : 1,
    }
    static { Object.freeze(this.EventId); }
    
    static EventIdCount = 2;
    static { Object.freeze(this.EventIdCount); }
    
    static StateId = 
    {
        ROOT : 0,
        GROUP : 1,
        G1 : 2,
        G2 : 3,
        S1 : 4,
    }
    static { Object.freeze(this.StateId); }
    
    static StateIdCount = 5;
    static { Object.freeze(this.StateIdCount); }
    
    // Used internally by state machine. Feel free to inspect, but don't modify.
    stateId;
    
    // Starts the state machine. Must be called before dispatching events. Not thread safe.
    start()
    {
        this.#ROOT_enter();
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
                this.#GROUP_enter();
                
                // group.<InitialState> behavior
                // uml: TransitionTo(g1)
                {
                    // Step 1: Exit states until we reach `group` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
                    
                    // Step 2: Transition action: ``.
                    
                    // Step 3: Enter/move towards transition target `g1`.
                    this.#G1_enter();
                    
                    // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
                    return;
                } // end of behavior for group.<InitialState>
            } // end of behavior for ROOT.<InitialState>
        } // end of behavior for ROOT
    }
    
    // Dispatches an event to the state machine. Not thread safe.
    // Note! This function assumes that the `eventId` parameter is valid.
    dispatchEvent(eventId)
    {
        switch (this.stateId)
        {
            // STATE: RocketSm
            case RocketSm.StateId.ROOT:
                switch (eventId)
                {
                    // Events not handled by this state:
                    case RocketSm.EventId.EV1: break;
                    case RocketSm.EventId.EV2: break;
                }
                break;
            
            // STATE: group
            case RocketSm.StateId.GROUP:
                switch (eventId)
                {
                    case RocketSm.EventId.EV1: this.#GROUP_ev1(); break;
                    // Events not handled by this state:
                    case RocketSm.EventId.EV2: break;
                }
                break;
            
            // STATE: g1
            case RocketSm.StateId.G1:
                switch (eventId)
                {
                    case RocketSm.EventId.EV1: this.#G1_ev1(); break;
                    // Events not handled by this state:
                    case RocketSm.EventId.EV2: break;
                }
                break;
            
            // STATE: g2
            case RocketSm.StateId.G2:
                switch (eventId)
                {
                    case RocketSm.EventId.EV2: this.#G2_ev2(); break;
                    // Events not handled by this state:
                    case RocketSm.EventId.EV1: this.#GROUP_ev1(); break; // First ancestor handler for this event
                }
                break;
            
            // STATE: s1
            case RocketSm.StateId.S1:
                switch (eventId)
                {
                    // Events not handled by this state:
                    case RocketSm.EventId.EV1: break;
                    case RocketSm.EventId.EV2: break;
                }
                break;
        }
        
    }
    
    // This function is used when StateSmith doesn't know what the active leaf state is at
    // compile time due to sub states or when multiple states need to be exited.
    #exitUpToStateHandler(desiredState)
    {
        while (this.stateId != desiredState)
        {
            switch (this.stateId)
            {
                case RocketSm.StateId.GROUP: this.#GROUP_exit(); break;
                
                case RocketSm.StateId.G1: this.#G1_exit(); break;
                
                case RocketSm.StateId.G2: this.#G2_exit(); break;
                
                case RocketSm.StateId.S1: this.#S1_exit(); break;
                
                default: return;  // Just to be safe. Prevents infinite loop if state ID memory is somehow corrupted.
            }
        }
    }
    
    
    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state ROOT
    ////////////////////////////////////////////////////////////////////////////////
    
    #ROOT_enter()
    {
        this.stateId = RocketSm.StateId.ROOT;
    }
    
    
    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state GROUP
    ////////////////////////////////////////////////////////////////////////////////
    
    #GROUP_enter()
    {
        this.stateId = RocketSm.StateId.GROUP;
    }
    
    #GROUP_exit()
    {
        this.stateId = RocketSm.StateId.ROOT;
    }
    
    #GROUP_ev1()
    {
        // group behavior
        // uml: EV1 TransitionTo(s1)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
            this.#exitUpToStateHandler(RocketSm.StateId.ROOT);
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `s1`.
            this.#S1_enter();
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            return;
        } // end of behavior for group
        
        // No ancestor handles this event.
    }
    
    
    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state G1
    ////////////////////////////////////////////////////////////////////////////////
    
    #G1_enter()
    {
        this.stateId = RocketSm.StateId.G1;
    }
    
    #G1_exit()
    {
        this.stateId = RocketSm.StateId.GROUP;
    }
    
    #G1_ev1()
    {
        let consume_event = false;
        
        // g1 behavior
        // uml: EV1 [a > 20] TransitionTo(g2)
        if (a > 20)
        {
            // Step 1: Exit states until we reach `group` state (Least Common Ancestor for transition).
            this.#G1_exit();
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `g2`.
            this.#G2_enter();
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            return;
        } // end of behavior for g1
        
        // Check if event has been consumed before calling ancestor handler.
        if (!consume_event)
        {
            this.#GROUP_ev1();
        }
    }
    
    
    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state G2
    ////////////////////////////////////////////////////////////////////////////////
    
    #G2_enter()
    {
        this.stateId = RocketSm.StateId.G2;
    }
    
    #G2_exit()
    {
        this.stateId = RocketSm.StateId.GROUP;
    }
    
    #G2_ev2()
    {
        // g2 behavior
        // uml: EV2 TransitionTo(g1)
        {
            // Step 1: Exit states until we reach `group` state (Least Common Ancestor for transition).
            this.#G2_exit();
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `g1`.
            this.#G1_enter();
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            return;
        } // end of behavior for g2
        
        // No ancestor handles this event.
    }
    
    
    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state S1
    ////////////////////////////////////////////////////////////////////////////////
    
    #S1_enter()
    {
        this.stateId = RocketSm.StateId.S1;
    }
    
    #S1_exit()
    {
        this.stateId = RocketSm.StateId.ROOT;
    }
    
    // Thread safe.
    static stateIdToString(id)
    {
        switch (id)
        {
            case RocketSm.StateId.ROOT: return "ROOT";
            case RocketSm.StateId.GROUP: return "GROUP";
            case RocketSm.StateId.G1: return "G1";
            case RocketSm.StateId.G2: return "G2";
            case RocketSm.StateId.S1: return "S1";
            default: return "?";
        }
    }
    
    // Thread safe.
    static eventIdToString(id)
    {
        switch (id)
        {
            case RocketSm.EventId.EV1: return "EV1";
            case RocketSm.EventId.EV2: return "EV2";
            default: return "?";
        }
    }
}
