// Autogenerated with StateSmith.
// Algorithm: Balanced2. See https://github.com/StateSmith/StateSmith/wiki/Algorithms

// Generated state machine
public class RocketSm
{
    public enum EventId
    {
        DO, // The `do` event is special. State event handlers do not consume this event (ancestors all get it too) unless a transition occurs.
    }
    
    public final int EventIdCount = 1;
    
    public enum StateId
    {
        ROOT,
        GROUP,
        G1,
        G2,
    }
    
    public final int StateIdCount = 4;
    
    // Used internally by state machine. Feel free to inspect, but don't modify.
    public StateId stateId;
    
    // State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
    public RocketSm()
    {
    }
    
    // Starts the state machine. Must be called before dispatching events. Not thread safe.
    public void start()
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
    public void dispatchEvent(EventId eventId)
    {
        
        switch (this.stateId)
        {
            // STATE: RocketSm
            case ROOT:
                // state and ancestors have no handler for `do` event.
                break;
            
            // STATE: group
            case GROUP:
                // state and ancestors have no handler for `do` event.
                break;
            
            // STATE: g1
            case G1:
                G1_do(); 
                break;
            
            // STATE: g2
            case G2:
                G2_do(); 
                break;
        }
        
    }
    
    // This function is used when StateSmith doesn't know what the active leaf state is at
    // compile time due to sub states or when multiple states need to be exited.
    private void exitUpToStateHandler(StateId desiredState)
    {
        while (this.stateId != desiredState)
        {
            switch (this.stateId)
            {
                case GROUP: GROUP_exit(); break;
                
                case G1: G1_exit(); break;
                
                case G2: G2_exit(); break;
                
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
    
    private void G1_do()
    {
        // g1 behavior
        // uml: do TransitionTo(g2)
        {
            // Step 1: Exit states until we reach `group` state (Least Common Ancestor for transition).
            G1_exit();
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `g2`.
            G2_enter();
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            return;
        } // end of behavior for g1
        
        // No ancestor handles this event.
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
    
    private void G2_do()
    {
        // g2 behavior
        // uml: do [x > 50] TransitionTo(g1)
        if (x > 50)
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
    
    // Thread safe.
    public static String stateIdToString(StateId id)
    {
        switch (id)
        {
            case ROOT: return "ROOT";
            case GROUP: return "GROUP";
            case G1: return "G1";
            case G2: return "G2";
            default: return "?";
        }
    }
    
    // Thread safe.
    public static String eventIdToString(EventId id)
    {
        switch (id)
        {
            case DO: return "DO";
            default: return "?";
        }
    }
}
