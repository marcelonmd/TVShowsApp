
window.scrollMonitor = {
    blazorComponent: null,
    isLoading: false,

    initialize: function (blazorComponentReference) {
        this.blazorComponent = blazorComponentReference;
        window.addEventListener('scroll', this.onScroll);
        this.onScroll();
    },

    dispose: function () {
        window.removeEventListener('scroll', this.onScroll);
        this.blazorComponent = null;
    },

    onScroll: function () {
        const scrollThreshold = 100;
        if ((window.innerHeight + window.scrollY) >= (document.body.offsetHeight - scrollThreshold) && !scrollMonitor.isLoading) {
            scrollMonitor.isLoading = true;
            if (scrollMonitor.blazorComponent) {
                scrollMonitor.blazorComponent.invokeMethodAsync('LoadMoreShows')
                    .then(() => {
                        scrollMonitor.isLoading = false;
                    })
                    .catch(error => {
                        console.error('Erro ao chamar LoadMoreShows no Blazor:', error);
                        scrollMonitor.isLoading = false;
                    });
            }
        }
    },

    setLoadingState: function (loading) {
        this.isLoading = loading;
    }
};