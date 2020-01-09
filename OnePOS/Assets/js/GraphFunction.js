function GraphFunction() {

    angular.module("app", ["chart.js"]).controller("homeChart", function ($scope, $http) {

        /////////////////////////////
        var w = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        var m = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

        $scope.copyWeekLabel = [];
        $scope.copyWeekData = [];
        $scope.copyMonthLabel = [];
        $scope.copyMonthData = [];


        $scope.labelIncome = [];
        $scope.dataIncome = [];

        $scope.optionList = [{ id: 1, value: "Weekly" }, { id: 2, value: "Monthly" }];
       
        $scope.ddValue = $scope.optionList[0];


        $scope.colors = ['#45b7cd', '#ff6384', '#ff8e72'];


        $scope.init = function () {

            $http({
                method: "GET",
                url: "ApiCollection/HistoryTransaction"
            }).then(function onSuccess(response) {

                var collectJson = JSON.parse(response.data.datajson);

                $scope.copyWeekLabel = collectJson.map(function (label) {
                    
                    return w[label.Day];
                });
                $scope.copyWeekData = collectJson.map(function (label) {
                    return label.TotalPayment;
                });

                $scope.labelIncome = $scope.copyWeekLabel;
                $scope.dataIncome = $scope.copyWeekData;
                
                $scope.options = {
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem, data) {
                                return "Total Payment: " + setNumbro(tooltipItem.yLabel);
                            }
                        }
                    },
                    scales: {
                        yAxes: [
                            {
                                ticks: {
                                    callback: function (label, index, labels) {
                                        return setNumbro(label);
                                    }
                                }
                            }
                        ]
                    }
                };

            }).catch(function onError(response) {

            });
        }

        $scope.ddFunction = function (action) {

            if (action.id == 1) {
                $scope.dataIncome = $scope.copyWeekData;
                $scope.labelIncome = $scope.copyWeekLabel;
            } else {
                if ($scope.copyMonthData.length == []) {

                    $http({
                        method: "GET",
                        url: "ApiCollection/MonthHistoryTransaction"
                    }).then(function onSuccess(response) {

                        var collectJson = JSON.parse(response.data.datajson);

                        $scope.copyMonthLabel = collectJson.map(function (label) {
                            return m[label.Month ];
                        });

                        $scope.copyMonthData = collectJson.map(function (label) {
                            return label.TotalPayment;
                        });

                        $scope.labelIncome = $scope.copyMonthLabel;
                        $scope.dataIncome = $scope.copyMonthData;

                        
                    }).catch(function onError(response) {

                    });
                } else {

                    $scope.labelIncome = $scope.copyMonthLabel;
                    $scope.dataIncome = $scope.copyMonthData;
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        $scope.barList = [
            { id: 1, value: "January" },
            { id: 2, value: "February" },
            { id: 3, value: "March" },
            { id: 4, value: "April" },
            { id: 5, value: "May" },
            { id: 6, value: "June" },
            { id: 7, value: "July" },
            { id: 8, value: "August" },
            { id: 9, value: "September" },
            { id: 10, value: "October" },
            { id: 11, value: "November" },
            { id: 12, value: "December" }
        ];
        $scope.barValue = $scope.barList[0];

        var itemJson;
        $scope.labelItem = [];
        $scope.dataItem = [];

        var setItemList = function (actionId) {

            var itemFilter = itemJson.filter(function (data) {
                return data.Month == actionId;
            });

            $scope.labelItem = itemFilter.map(function (label) {
                return label.ItemName;
            });

            $scope.dataItem = itemFilter.map(function (label) {
                return label.TotalItem;
            });

            $scope.options2 = {
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            return "Total Purchased Items: " + setNumbro(tooltipItem.yLabel);
                        }
                    }
                },
                scales: {
                    yAxes: [
                        {
                            ticks: {
                                callback: function (label, index, labels) {
                                    return setNumbro(label);
                                }
                            }
                        }
                    ]
                }
            };

        }

        $scope.itemInit = function () {
            $http({
                method: "GET",
                url: "ApiCollection/ItemHistoryTransaction"
            }).then(function onSuccess(response) {

                var collectJson = JSON.parse(response.data.datajson);

                itemJson = collectJson;

                setItemList(1);
            }).catch(function onError(response) {

            });
        }

        $scope.labelTotalAsset = [];
        $scope.dataTotalAsset = [];

        $http({
            method: "GET",
            url: "ApiCollection/TotalAssets"
        }).then(function onSuccess(response) {
            //console.log(response.data.datajson);
            var collectJson = JSON.parse(response.data.datajson);

            itemJson = collectJson;
            
            $scope.labelTotalAsset = [
                "Buy Price",
                "Sale Price"
            ];

            $scope.dataTotalAsset = itemJson.map(function (label) {
                return label.TotalAsset;
            });
            $scope.options3 = {
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            return "Total Assets: " + setNumbro(tooltipItem.yLabel);
                        }
                    }
                },
                scales: {
                    yAxes: [
                        {
                            ticks: {
                                callback: function (label, index, labels) {
                                    return setNumbro(label);
                                }
                            }
                        }
                    ]
                }
            };
        }).catch(function onError(response) {

        });

        $scope.changeDataFunction = function (action) {
            $scope.labelItem = [];
            $scope.dataItem = [];

            setItemList(action.id);
        };
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        $scope.labels2 = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
        $scope.data2 = [300, 500, 100];
    });
}
