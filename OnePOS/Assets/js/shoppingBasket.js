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
	        $scope.dope = 0;
            
			$scope.primaryUrl = mainJsonUrl;
			$scope.init = function() {
			    $http({
			        method: "GET",
			        url: $scope.primaryUrl
			    }).then(function (data, status) {
			        var collectJson = JSON.parse(data.data.datajson);
			        for (var i = 0; i < collectJson.length; i++) {
			            
			            
			            var collectOriginalJson = {
			                ItemId: collectJson[i].ItemId,
			                ItemPrice: collectJson[i].ItemPrice,
			                ItemName: collectJson[i].ItemName,
			                TotalStock: collectJson[i].TotalStock,
			                ItemIndex : i
			            }
			            $scope.itemCollections.push(collectOriginalJson);
			            

			            var cloneItem = {
                            id : i+1,
			                cloneId: collectJson[i].ItemId,
			                clonePrice: collectJson[i].ItemPrice,
			                cloneName: collectJson[i].ItemName,
			                cloneStock: collectJson[i].TotalStock,
                            cloneIndex : i
			            }

			            $scope.tempItemColls.push(cloneItem);
			        }
			       

			    }, function error() {

			    });
			}

	        
	        $scope.order = [];

	        $scope.add = function (itemIndex,itemId) {
	            var orderItem;
	            var nihil = false;
	            $('#messageLoader').hide();
	            checkWarning(false);

	            if ($scope.itemCollections[itemIndex].ItemId == itemId) {
	                var tempItem = $scope.itemCollections[itemIndex];

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


	                        } else {
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
	                                totalPrice: tempItem.ItemPrice,
	                                isOpen: false,
                                    collectionIndex : itemIndex
	                            };
	                            tempItem.TotalStock--;
	                            $scope.order.push(orderItem);
	                        } else {
	                            $('#messageLoader').show();
	                            checkWarning(true);
	                        }

	                    }
	                } else {
	                    if (tempItem.TotalStock > 0) {
	                        orderItem = {
	                            id: tempItem.ItemId,
	                            name: tempItem.ItemName,
	                            qty: 1,
	                            price: tempItem.ItemPrice,
	                            totalPrice: tempItem.ItemPrice,
	                            isOpen: false,
	                            collectionIndex: itemIndex
	                        };
	                        tempItem.TotalStock--;
	                        $scope.order.push(orderItem);
	                    } else {
	                        $('#messageLoader').show();
	                        checkWarning(true);
	                    }
	                }
	                $scope.ctrIndex++;
	            }
	        };
			
	        $scope.deleteItem = function (index, itemId, itemQty, collectionIndex) {
	            
	            if ($scope.itemCollections[collectionIndex].ItemId == itemId) {
	                $scope.itemCollections[collectionIndex].TotalStock = $scope.tempItemColls[collectionIndex].cloneStock;
	            }
	            
	            $scope.order.splice(index, 1);
	        };
		
	        $scope.editItem = function (itemId, itemQty, idx, collectionIndex) {
	            

	            if ($scope.itemCollections[collectionIndex].ItemId == itemId) {
	                var tempColletion = $scope.itemCollections[collectionIndex];
	                var tempItemCol = $scope.tempItemColls[collectionIndex];

	                if ($scope.order[idx].id == itemId) {
	                    if (tempItemCol.cloneStock >= itemQty) {
	                        tempColletion.TotalStock = tempItemCol.cloneStock - itemQty;
	                        $scope.order[idx].qty = itemQty;
	                        $scope.order[idx].totalPrice = $scope.order[idx].price * itemQty;

	                        $scope.order[idx].isOpen = false;

	                        $('#exceedsStock').hide();
	                        checkWarning(false);
	                    } else {
	                        $('#exceedsStock').show();
	                        checkWarning(true);
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
	            $scope.ctrIndex = 0;
	        };
            
	        $scope.qtyModified = {

	            open: function open(idx) {
	                
	                $('#exceedsStock').hide();
	                checkWarning(false);
	                $scope.order[idx].isOpen = true;
	                
	            },

	            close: function close(idx) {
	                
	                $('#exceedsStock').hide();
	                checkWarning(false);

	                $scope.order[idx].isOpen = false;
	                
	            }
	        };

	
		}
	]);

}

var transactionData = {
    transactionItem: {
        value: ""
    },
    transactionQuantity: {
        value: ""
    },
    transactionTotalPayment: {
        value : ""
    },
    transactionTotal : {
        value : ""   
    }
};

function functionAddTransaction() {
    //submit data to db

    $('.add-transaction-btn').click(function (evt) {
        evt.preventDefault();
        var transactionTotal = 0;


        transactionData.transactionItem.value = [];
        $('#transactionTable tbody tr').each(function () {

            transactionData.transactionItem.value.push($(this).find('.transaction-item-id').text());

        });
        
        transactionData.transactionQuantity.value = [];
        $('#transactionTable tbody tr').each(function () {
            
            transactionData.transactionQuantity.value.push($(this).find('.transaction-item-quantity').text());
            transactionTotal += parseInt($(this).find('.transaction-item-quantity').text());
        });
        
        document.getElementById("TransactionItem").value = transactionData.transactionItem.value.join('|');
        document.getElementById("TransactionQuantity").value = transactionData.transactionQuantity.value.join('|');
        document.getElementById("TransactionTotal").value = transactionTotal + "";
        document.getElementById("TransactionTotalPayment").value = parseInt($('.transaction-price').html()) + "";


        $('#ShoppingBasketPost').submit();


    });
}




