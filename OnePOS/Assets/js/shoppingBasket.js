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
			$scope.dopeQty = 0;
			$scope.dopeDsc = 0;
			$scope.transDsc = 0;
			$scope.reduceVal = 0;
			var isGetSum = false;
	        $scope.valueSum = 0;


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
	            isGetSum = true;
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
	                                isQtyOpen: false,
	                                collectionIndex: itemIndex,
	                                discount: 0,
	                                isDscOpen: false,
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
	                            isQtyOpen: false,
	                            collectionIndex: itemIndex,
	                            discount: 0,
	                            isDscOpen: false,
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
	                isGetSum = true;
	                $scope.itemCollections[collectionIndex].TotalStock = $scope.tempItemColls[collectionIndex].cloneStock;
	            }
	            
	            $scope.order.splice(index, 1);
	        };
		
	        $scope.editItem = function (itemId, itemQty, idx, collectionIndex) {
	            isGetSum = true;

	            if ($scope.itemCollections[collectionIndex].ItemId == itemId) {
	                var tempColletion = $scope.itemCollections[collectionIndex];
	                var tempItemCol = $scope.tempItemColls[collectionIndex];
	                var currentDiscount = 0;
	                var currentPrice = 0;
	                if ($scope.order[idx].id == itemId) {
	                    if (tempItemCol.cloneStock >= itemQty) {
	                        tempColletion.TotalStock = tempItemCol.cloneStock - itemQty;
	                        $scope.order[idx].qty = itemQty;
	                        currentPrice = $scope.order[idx].price * itemQty;
	                        currentDiscount = (currentPrice * $scope.order[idx].discount) / 100;
	                        $scope.order[idx].totalPrice = currentPrice - currentDiscount - $scope.reduceVal;

	                        $scope.order[idx].isQtyOpen = false;

	                        $('#exceedsStock').hide();
	                        checkWarning(false);
	                    } else {
	                        $('#exceedsStock').show();
	                        checkWarning(true);
	                    }
	                }

	            }
	        };

			
	        $scope.getSum = function () {
               if (isGetSum) {
                   var i = 0,
				   sum = 0, cDiscount=0;
                    for (; i < $scope.order.length; i++) {
                        sum += parseInt($scope.order[i].totalPrice);

                    }
                    cDiscount = (sum * $scope.transDsc) / 100;

                   $scope.valueSum = sum - cDiscount - $scope.reduceVal;
                    isGetSum = false;
               }
	        };
			
	        $scope.clearOrder = function () {
	            isGetSum = true;
	            $scope.order = [];
	            $scope.ctrIndex = 0;
	        };
	        $scope.adjustDsc = function (itemId, discount, idx, collectionIndex) {
                

	            if ($scope.itemCollections[collectionIndex].ItemId == itemId) {
	                isGetSum = true;
	                var currentDiscount = 0;
	                var currentPrice = 0;
	                if ($scope.order[idx].id == itemId) {
	                    $scope.order[idx].discount = discount;
	                    currentPrice = $scope.order[idx].price * $scope.order[idx].qty;
	                    currentDiscount = (currentPrice * discount) / 100;

	                    $scope.order[idx].totalPrice = currentPrice - currentDiscount;
	                    $scope.order[idx].isDscOpen = false;
	                }

	            }
	        };

	        $scope.transCalcDsc = function (checkNul) {
	            if (checkNul != null) {
	                isGetSum = true;
	            }
	            
	        }
            
	        $scope.reduceInvoiceVal = function (checkNul) {
	            if (checkNul != null) {
	                isGetSum = true;
	            }

	        }

	        $scope.qtyModified = {

	            open: function open(idx) {
	                
	                $('#exceedsStock').hide();
	                checkWarning(false);
	                $scope.order[idx].isQtyOpen = true;
	            },

	            close: function close(idx) {
	                
	                $('#exceedsStock').hide();
	                checkWarning(false);

	                $scope.order[idx].isQtyOpen = false;
	                
	            }
	        };
            
	        $scope.dscModified = {

	            open: function open(idx) {

	                //$('#exceedsStock').hide();
	                //checkWarning(false);
	                $scope.order[idx].isdscOpen = true;

	            },

	            close: function close(idx) {

	                //$('#exceedsStock').hide();
	                //checkWarning(false);

	                $scope.order[idx].isDscOpen = false;

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
    },
    discountPerItems: {
        value: ""
    },
    discountTotalTransaction: {
        value: ""
    },
    reduceInvoiceValue: {
        value: ""
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

        transactionData.discountPerItems.value = [];        
        $('#transactionTable tbody tr').each(function () {

            transactionData.discountPerItems.value.push($(this).find('.transaction-item-discount').text());

        });


        document.getElementById("TransactionItem").value = transactionData.transactionItem.value.join('|');
        document.getElementById("TransactionQuantity").value = transactionData.transactionQuantity.value.join('|');
        document.getElementById("TransactionTotal").value = transactionTotal + "";
        document.getElementById("TransactionTotalPayment").value = $('.transaction-price').val() + "";
        document.getElementById("DiscountPerItems").value = transactionData.discountPerItems.value.join('|');
        document.getElementById("DiscountTotalTransaction").value = $('.discount-total-transaction').val() + "";
        document.getElementById("ReduceInvoiceValue").value = $('.reduce-total-transaction').val() + "";

        $('#ShoppingBasketPost').submit();
    });
}




