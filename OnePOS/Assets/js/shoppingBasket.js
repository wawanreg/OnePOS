var shoppingCollection = function (classBelowWarning,mainJsonUrl) {
    var checkWarning = function (isWarning) {
        if (isWarning)
            $(classBelowWarning).css('margin-top', '10px');
        else
            $(classBelowWarning).css('margin-top', '');
    }

    var app = angular.module('app', ['ngResource', 'angularUtils.directives.dirPagination']);

	app.controller('POSController',['$scope', '$http', function ($scope,$http) {
			$scope.itemColletions = [];
			
			$scope.primaryUrl = mainJsonUrl;
			$scope.init = function() {
			    $http({
			        method: "GET",
			        url: $scope.primaryUrl
			    }).then(function (data, status) {
			        var collectJson = JSON.parse(data.data.datajson);
			        for (var i = 0; i < collectJson.length; i++) {
			            $scope.itemColletions.push(collectJson[i]);
			        }
			    }, function error() {

			    });
			}

	        
	        $scope.order = [];

	        $scope.add = function (itemId) {
	            var orderItem;
	            var nihil = false;
	            $('#messageLoader').hide();
	            checkWarning(false);

	            for (var i = 0; i < $scope.itemColletions.length; i++) {
	                if ($scope.itemColletions[i].ItemId == itemId) {
	                    var tempItem = $scope.itemColletions[i];

	                    if ($scope.order.length > 0) {
	                        for (var j = 0; j < $scope.order.length; j++) {
	                            if ($scope.order[j].id == tempItem.ItemId) {
	                                
	                                if (tempItem.TotalStock > 0) {
	                                    $scope.order[j].qty += 1;
	                                    nihil = false;
	                                    tempItem.TotalStock--;
	                                    break;
	                                } else {
	                                    $('#messageLoader').show();
	                                    checkWarning(true);
	                                }
	                                
	                                
	                            }else {
	                                nihil = true;
	                            }
	                        }
	                        if (nihil) {
	                            if (tempItem.TotalStock > 0) {
	                                orderItem = {
	                                    id: tempItem.ItemId,
	                                    name: tempItem.ItemName,
	                                    qty: 1,
	                                    price: tempItem.ItemPrice
	                                };
	                                tempItem.TotalStock--;
	                                $scope.order.push(orderItem);
	                            } else {
	                                $('#messageLoader').show();
	                                checkWarning(true);
	                            }
	                            
	                        }
	                    } else {
	                        orderItem = {
	                            id: tempItem.ItemId,
	                            name: tempItem.ItemName,
	                            qty: 1,
	                            price: tempItem.ItemPrice
	                        };
	                        tempItem.TotalStock--;
	                        $scope.order.push(orderItem);
	                    }
                    }
	                
	            }
	        };
			
			
			
			
			// $scope.getSum = function() {
			  // var i = 0,
				// sum = 0;
			  // for(; i < $scope.order.length; i++) {
				// sum += parseInt($scope.order[i].item.price, 10);
			  // }
			  // return sum;
			// };
			
			// $scope.deleteItem = function(index) {
			  // $scope.order.splice(index,1);
			// };
			
			// $scope.checkout = function(index) {
			  // alert("Order total: $" + $scope.getSum() + "\n\nPayment received. Thanks.");
			// };
			
			// $scope.clearOrder = function() {
			  // $scope.order = [];
			// };
		}
	]);
}


