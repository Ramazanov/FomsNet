namespace Foms.CoreDomain.LoanCycles
{
    public class RateCycle:Cycle
    {
       public RateCycle() {}

       public RateCycle(int id, int loanCycle, decimal min, decimal max, int cycleObjectId, int cycleId)
       {
           Id = id;
           LoanCycle = loanCycle;
           Min = min;
           Max = max;
           CycleObjectId = cycleObjectId;
           CycleId = cycleId;
       }
    }
}
