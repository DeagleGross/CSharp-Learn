using System;
using System.Threading.Tasks;

namespace DissectingAsyncMethods
{
    public class GetStockPriceForAsync_StateMachine
    {
        /*
         * Here state-machine is defined. There are a couple of states, initial state is established.
         * Also the continuation is implemented using TaskCompletionSource -> _tcs
         */
        enum State { Start, Step1, }
        private readonly StockPrices @this;
        private readonly string _companyId;
        private readonly TaskCompletionSource<decimal> _tcs;
        private Task _initializeMapIfNeededTask;
        private State _state = State.Start;

        public GetStockPriceForAsync_StateMachine(StockPrices @this, string companyId)
        {
            this.@this = @this;
            _companyId = companyId;
        }

        public void Start()
        {
            try
            {
                switch (_state)
                {
                    case State.Start:
                    {
                        // The code from the start of the method to the first 'await'.

                        if (string.IsNullOrEmpty(_companyId))
                            throw new ArgumentNullException();

                        _initializeMapIfNeededTask = @this.InitializeMapIfNeededAsync();

                        // Update state and schedule continuation
                        _state = State.Step1;
                        _initializeMapIfNeededTask.ContinueWith(_ => Start());
                        
                        break;
                    }

                    case State.Step1:
                    {
                        // Need to check the error and the cancel case first
                        if (_initializeMapIfNeededTask.Status == TaskStatus.Canceled)
                            _tcs.SetCanceled();
                        else if (_initializeMapIfNeededTask.Status == TaskStatus.Faulted)
                            _tcs.SetException(_initializeMapIfNeededTask.Exception.InnerException);
                        else
                        {
                            // The code between first await and the rest of the method

                            @this._stockPrices.TryGetValue(_companyId, out var result);
                            _tcs.SetResult(result);
                        }

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                _tcs.SetException(e);
            }
        }

        public Task<decimal> Task => _tcs.Task;

        public async Task<decimal> GetStockPriceForAsync(string companyId)
        {
            var stateMachine = new GetStockPriceForAsync_StateMachine(@this, companyId);
            stateMachine.Start();
            return stateMachine.Task.Result;
        }
    }
}
