var shoppingCollection = function (classBelowWarning,mainJsonUrl) {

    

    var checkWarning = function (isWarning) {
        if (isWarning)
            $(classBelowWarning).css('margin-top', '10px');
        else
            $(classBelowWarning).css('margin-top', '');
    }

    var app = angular.module('app', ['ngResource', 'angularUtils.directives.dirPagination', 'ui.bootstrap']);

	app.controller('POSController',['$scope', '$http', function ($scope,$http) {
			$scope.itemCollections = [];
			$scope.tempItemColls = [];

			$scope.primaryUrl = mainJsonUrl;
			$scope.init = function() {
			    $http({
			        method: "GET",
			        url: $scope.primaryUrl
			    }).then(function (data, status) {
			        var collectJson = JSON.parse(data.data.datajson);
			        for (var i = 0; i < collectJson.length; i++) {
			            
			            $scope.itemCollections.push(collectJson[i]);

			            var cloneItem = {
                            id : i+1,
			                cloneId: collectJson[i].ItemId,
			                clonePrice: collectJson[i].ItemPrice,
			                cloneName: collectJson[i].ItemName,
			                cloneStock: collectJson[i].TotalStock
			            }

			            $scope.tempItemColls.push(cloneItem);
			        }
			        console.log($scope.tempItemColls);

			    }, function error() {

			    });
			}

	        
	        $scope.order = [];

	        $scope.add = function (itemId) {
	            var orderItem;
	            var nihil = false;
	            $('#messageLoader').hide();
	            checkWarning(false);

	            for (var i = 0; i < $scope.itemCollections.length; i++) {
	                if ($scope.itemCollections[i].ItemId == itemId) {
	                    var tempItem = $scope.itemCollections[i];

	                    if ($scope.order.length > 0) {
	                        for (var j = 0; j < $scope.order.length; j++) {
	                            if ($scope.order[j].id == tempItem.ItemId) {
	                                
	                                if (tempItem.TotalStock > 0) {
	                                    $scope.order[j].qty += 1;
	                                    $scope.order[j].totalPrice += tempItem.ItemPrice;

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
	                                    price: tempItem.ItemPrice,
	                                    totalPrice: tempItem.ItemPrice
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
	                            price: tempItem.ItemPrice,
	                            totalPrice: tempItem.ItemPrice
	                        };
	                        tempItem.TotalStock--;
	                        console.log($scope.tempItemColls);
	                        $scope.order.push(orderItem);
	                    }
                    }
	                
	            }
	        };
			
	        $scope.deleteItem = function (index,itemId,itemQty) {
	            
	            for (var i = 0; i < $scope.itemCollections.length; i++) {
	                if ($scope.itemCollections[i].ItemId == itemId) {
	                    $scope.itemCollections[i].TotalStock += itemQty;
	                }
	            }
	            $scope.order.splice(index, 1);
	        };
		
	        $scope.editItem = function (itemId, itemQty) {
	            

	            for (var i = 0; i < $scope.itemCollections.length; i++) {
	                if ($scope.itemCollections[i].ItemId == itemId) {
	                    var tempColletion = $scope.itemCollections[i];
	                    var tempItemCol = $scope.tempItemColls[i];

	                    for (var j = 0; j < $scope.order.length; j++) {
	                        if ($scope.order[j].id == itemId) {
	                            

	                            if (tempColletion.TotalStock >= itemQty) {
	                                tempColletion.TotalStock = tempItemCol.TotalStock - itemQty;
                                    $scope.order[j].qty = itemQty;
	                                $scope.order[j].totalPrice = $scope.order[i].price * itemQty;

	                            } else {
	                                $scope.order[j].qty = tempColletion.TotalStock;
	                               

	                            }
	                            
	                        }

	                    }
                    }

	                
                }
	            

	        };

			
			 $scope.getSum = function() {
			   var i = 0,
				 sum = 0;
			   for(; i < $scope.order.length; i++) {
			       sum += parseInt($scope.order[i].totalPrice);
			       
			   }
			   return sum;
			 };
			
	        $scope.clearOrder = function() {
	            $scope.order = [];
	        };	
		}
	]);

	//app.directive('bsPopover', function () {
	//    return function (scope, element, attrs) {
	//        console.log("trigger");
	//        element.find("a[data-toggle=mainTest]").popover();
	//    };
	//});
}


