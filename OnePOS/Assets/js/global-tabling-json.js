var globalTablingJson = function (tableName, classBelowWarning, mainJsonUrl, searchJsonUrl,autoOn) {
    
    var tableShow = function (isAction) {
		if (isAction)
		    $(tableName).removeClass("fadeout").addClass("fadein");
		else
		    $(tableName).removeClass("fadein").addClass("fadeout");
	};

	var checkWarning = function (isWarning) {
	    if (isWarning)
	        $(classBelowWarning).css('margin-top', '10px');
	    else
	        $(classBelowWarning).css('margin-top', '');
	}

    var functionExcel = function(isExcel) {
        //check if there excel button
        if (isExcel) {
            if ($('.excel-button').length != 0) {
                $('.excel-button').css('pointer-events', '');
                $('.excel-button').css('cursor', '; ');
            }
        } else {

            if ($('.excel-button').length != 0) {
                $('.excel-button').css('pointer-events', 'none');
                $('.excel-button').css('cursor', 'default; ');
            }
        }
    }

    var standarPackage = function (state, jsonLength) {
       
        if (state == "success") {
            $('.loader').hide(); tableShow(true); functionExcel(true);

            if (jsonLength == 0) {
                functionExcel(false);
                $('#messageLoader').show();
                checkWarning(true);
                tableShow(false);
            }
        }else if(state == "start")  {
            $('.loader').show(); $('#messageLoader').hide(); checkWarning(false); tableShow(false);
        }else if (state == "error") {
            $('.loader').hide();
            $('#messageLoader').hide();
            checkWarning(false);
            functionExcel(false);
            tableShow(false);
        }
    }
    
    

    var app = angular.module('app', ['ngResource', 'angularUtils.directives.dirPagination']);
	
	app.factory('service', [
		'$resource', function ($resource) {
			var factory = {};

			return factory;
		}
	]);
	app.controller('controller', [
		'$scope', '$http', function ($scope, $http) {
			
            //for transactionList
		    var dateNow = new Date();
		    
		    $scope.startDate = new Date(dateNow.getFullYear(), dateNow.getMonth(), 01);

		    dateNow.setHours(23);
		    dateNow.setMinutes(59);
		    dateNow.setSeconds(59);

		    $scope.endDate = dateNow;
		    
		    $scope.convertStringToDate = function (str) {
		        return new Date(str);
		    }
		    
		    $scope.getOffset = function() {
		        return new Date().getTimezoneOffset() / -60;
		    }
		    $scope.excMerchantId = 0;
		    $scope.excPromoId = 0;

		    $scope.passingDataToModal = function (mId, pId) {
                $scope.excMerchantId = mId;
                $scope.excPromoId = pId;
		    }

		    

            $scope.dateFilter = function (transaction) {
                var transactionDate = $scope.convertStringToDate(transaction.dateComparator);
		        return (transactionDate >= $scope.startDate && transactionDate <= $scope.endDate);
		    }
		    /////


		    $scope.page = 1;
		    $scope.take = 10;
			$scope.pageSize = 0;
			$scope.currentPage = 1;
			$scope.totalItem = 0;
			$scope.currentNumber = 1;
			$scope.jsonLists = [];
			$scope.keepPromoId = 0;
		    //for get information is data customer on or off
			$scope.isAllCustomer = false;
		    //for backenduser
			$scope.isFlagSet = false;

			var triggerPagging = false;
			var searchOn = false;
			
			$scope.primaryUrl = '';
			$scope.searchUrl = '';
			
			var dataListing = function (data) {
			    
			    var collectJson = JSON.parse(data.datajson);
			    $scope.pageSize = data.itemsPerPage;
			    $scope.jsonLists = [];

			    $scope.totalItem = data.itemsPerPage;

			    if ($scope.pageSize > 10) {
			        $scope.pageSize = 10;
			    }

			    for (var i = 0; i < collectJson.length; i++) {
			        $scope.jsonLists.push(collectJson[i]);
			    }
			    standarPackage("success", collectJson.length);
			    //console.log($scope.jsonLists);
			}


			$scope.init = function () {
			    $scope.primaryUrl = mainJsonUrl;
                //Set the url into searchUrl
			    $scope.searchUrl = searchJsonUrl;

			    if (autoOn) {
			        
			        standarPackage('start',0);

			        $http({
			            method: "GET",
			            url: $scope.primaryUrl + "?take=" + $scope.take + "&page=" + $scope.page
			        }).then(function onSuccess(response) {
			            triggerPagging = false;
			            
			            dataListing(response.data);
			            
			            

			        }).catch(function onError(response) {
			            console.log(response.statusText);
			            standarPackage("error", 0);
			        });
			    }
			}

            $scope.pageChangeHandler = function (num) {
                
				if (triggerPagging) {
				    standarPackage("start", 0);

				    //var theUrl;

				    //if (searchOn) {
				    //    if (parameter == '') {
				    //        theUrl = $scope.searchUrl + "?take=" + $scope.take + "&page=" + num + "&target=" + $('#searchValue').val() + "&promoid=" + $scope.keepPromoId;
				    //    }else {
				    //        theUrl = $scope.searchUrl + "?take=" + $scope.take + "&page=" + num + "&target=" + $('#searchValue').val() + parameter;
				    //    }   
				    //} else {
				    //    if (parameter == '') {
				    //        theUrl = $scope.primaryUrl + "?take=" + $scope.take + "&page=" + num + "&promoid=" + $scope.keepPromoId;
				    //    } else {
				    //        theUrl = $scope.primaryUrl + "?take=" + $scope.take + "&page=" + num + parameter;
				    //    }
				        
				    //}

					
					$http({
						method: "GET",
						url: $scope.primaryUrl + "?take=" + $scope.take + "&page=" + num
					}).then(function onSuccess(response) {
					    
						//var collectJson = JSON.parse(data.datajson);
						//$scope.pageSize = data.itemsPerPage;
						//$scope.jsonLists = [];

					    $scope.currentPage = num;
						$scope.currentNumber = (num * 10) - 9;
						$scope.totalItem = response.data.itemsPerPage;

					    dataListing(response.data);

					    
					}).catch(function onError(response) {
						triggerPagging = false;
					    standarPackage("error",0);
					});
				};
				triggerPagging = true;
			}
			//$scope.change = function (parameterString, promoId) {

		    //    $scope.keepPromoId = promoId;
		        
		    //    if (promoId != "") {

		    //        standarPackage("start",0);

		    //        $http({
		    //            method: "JSON",
		    //            url: $scope.primaryUrl + "?take=" + $scope.take + "&page=" + $scope.page + parameterString
		    //        }).success(function (data, status) {
		    //            triggerPagging = false;
		    //            searchOn = false;
		    //            var collectJson = JSON.parse(data.datajson);

		    //            $scope.currentPage = 1;
		    //            $scope.pageSize = data.itemsPerPage;
		    //            $scope.jsonLists = [];
		    //            $scope.currentNumber = 1;
		    //            $scope.totalItem = data.itemsPerPage;

		    //            if ($scope.pageSize > 10) {
		    //                $scope.pageSize = 10;
		    //            }

		    //            for (var i = 0; i < collectJson.length; i++) {
		    //                $scope.jsonLists.push(collectJson[i]);
		    //            }

		    //            //for get information is data customer on or off
		    //            console.log(collectJson[0]);
		    //            if (collectJson.length > 0 && collectJson[0].all != null)
    		//                $scope.isAllCustomer = collectJson[0].all;

		    //            standarPackage("success", collectJson.length);


		    //        }).error(function (data, status) {
		    //            triggerPagging = false;
		    //            standarPackage("error",0);
		    //        });
		    //    } else {
		    //        triggerPagging = false;
		    //        $scope.transactionLists = [];
		    //        standarPackage("error", 0);
		    //    }
		    //}

			//$scope.myFunct = function (keyEvent,extraParameter) {
			//	if (keyEvent.which === 13) {
			//	    standarPackage("start",0);
			//	    $scope.page = 1;

			//	    var parameterString = "&target=" + $('#searchValue').val();
				    
				    
            //        if(extraParameter != '')
            //            parameterString = "&target=" + $('#searchValue').val() + extraParameter;
                    

			//		$http({
			//			method: "JSON",
			//			url: $scope.searchUrl + "?take=" + $scope.take + "&page=" + $scope.page + parameterString
			//		}).success(function (data, status) {
			//		    triggerPagging = false;
			//			searchOn = true;
			//			$scope.currentPage = 1;
			//			$scope.currentNumber = 1;
			//			var collectJson = JSON.parse(data.datajson);
			//			$scope.pageSize = data.itemsPerPage;
			//			$scope.jsonLists = [];

			//			$scope.totalItem = data.itemsPerPage;

			//			if ($scope.pageSize > 10) {
			//				$scope.pageSize = 10;
			//			}

			//			for (var i = 0; i < collectJson.length; i++) {
			//				$scope.jsonLists.push(collectJson[i]);
			//			}

			//			standarPackage("success", collectJson.length);
			//		}).error(function (data, status) {
			//			triggerPagging = false;
			//		    standarPackage("error",0);
			//		});
			//	}

			//}
			//$scope.searchBtn = function (parameter) {
	        //    standarPackage("start", 0);
	        //    console.log(parameter);
	        //    $scope.page = 1;
	        //    $http({
	        //        method: "JSON",
	        //        url: $scope.primaryUrl + "?take=" + $scope.take + "&page=" + $scope.page + parameter
	        //    }).success(function (data, status) {
	        //        triggerPagging = false;
	        //        searchOn = false;
	        //        $scope.currentPage = 1;
	        //        $scope.currentNumber = 1;
	        //        var collectJson = JSON.parse(data.datajson);

	        //        $scope.pageSize = data.itemsPerPage;
	        //        $scope.jsonLists = [];

	        //        $scope.totalItem = data.itemsPerPage;

	        //        if ($scope.pageSize > 10) {
	        //            $scope.pageSize = 10;
	        //        }
	                
	        //        for (var i = 0; i < collectJson.length; i++) {
	        //            $scope.jsonLists.push(collectJson[i]);
	        //        }

	        //        if (collectJson.length > 0 && collectJson[0].iscustomer != null)
	        //            $scope.isFlagSet = collectJson[0].iscustomer;

	        //        standarPackage("success", collectJson.length);

	        //    }).error(function (data, status) {
	        //        triggerPagging = false;
	        //        standarPackage("error", 0);
	        //    });
			//};
            
			//$scope.pageChangeHandler = function (num, parameter) {
                
			//	if (triggerPagging) {
			//	    standarPackage("start", 0);

			//	    var theUrl;

			//	    if (searchOn) {
			//	        if (parameter == '') {
			//	            theUrl = $scope.searchUrl + "?take=" + $scope.take + "&page=" + num + "&target=" + $('#searchValue').val() + "&promoid=" + $scope.keepPromoId;
			//	        }else {
			//	            theUrl = $scope.searchUrl + "?take=" + $scope.take + "&page=" + num + "&target=" + $('#searchValue').val() + parameter;
			//	        }   
			//	    } else {
			//	        if (parameter == '') {
			//	            theUrl = $scope.primaryUrl + "?take=" + $scope.take + "&page=" + num + "&promoid=" + $scope.keepPromoId;
			//	        } else {
			//	            theUrl = $scope.primaryUrl + "?take=" + $scope.take + "&page=" + num + parameter;
			//	        }
				        
			//	    }

					
			//		$http({
			//			method: "JSON",
			//			url: theUrl
			//		}).success(function (data, status) {
					    
			//			var collectJson = JSON.parse(data.datajson);
			//			$scope.pageSize = data.itemsPerPage;
			//			$scope.jsonLists = [];
			//			$scope.currentPage = num;
			//			$scope.currentNumber = (num * 10) - 9;
			//			$scope.totalItem = data.itemsPerPage;

			//			if ($scope.pageSize > 10) {
			//				$scope.pageSize = 10;
			//			}

			//			for (var i = 0; i < collectJson.length; i++) {
			//				$scope.jsonLists.push(collectJson[i]);
			//			}
			//			if (collectJson[0].all != null)
			//			    $scope.isAllCustomer = collectJson[0].all;

			//			if (collectJson[0].iscustomer != null)
			//			    $scope.isFlagSet = collectJson[0].iscustomer;

			//		    standarPackage("success",collectJson.length);
			//		}).error(function (data, status) {
			//			triggerPagging = false;
			//		    standarPackage("error",0);
			//		});
			//	};
			//	triggerPagging = true;
			//}
	        
	    }
	]);
    

	app.filter('moment', function () {
		return function (dateString, format) {
			var mm;
			if (moment(dateString).isValid()) {
				mm = moment(dateString).format(format);
			} else {
				mm = "";
			}
			return mm;
		};
	});


}