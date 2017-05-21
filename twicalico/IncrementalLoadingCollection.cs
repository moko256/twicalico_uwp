using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;

namespace twicalico
{
    class IncrementalLoadingCollection<I, E> : ObservableCollection<I>, ISupportIncrementalLoading where E : IEnumerable<I>
    {
        private CoreDispatcher mainTask;

        public IncrementalLoadingCollection(CoreDispatcher mainTask){
            this.mainTask = mainTask;
        }

        public Func<long, Task<E>> LoadMoreTask { get; set; }
        public Func<I, long> makeId { get; set; }

        private async Task<E> LoadMore()
        {
            return await LoadMoreTask.Invoke(makeId(this.Last()));
        }

        bool isLoading = false;
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run(async () => {
                if (!isLoading && count!=0)
                {
                    isLoading = true;
                    var result = await LoadMore();
                    if (result.Count() > 0)
                    {
                        await mainTask.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            foreach (var s in result)
                            {
                                Add(s);
                            }
                        });
                        isLoading = false;
                    }
                    return new LoadMoreItemsResult() { Count = (uint)result.Count() };
                }
                else
                {
                    return new LoadMoreItemsResult() { Count = 0 };
                }
            }).AsAsyncOperation();
        }

        public bool HasMoreItems => Count != 0 && !isLoading;
    }
}
