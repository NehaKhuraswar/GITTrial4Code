'use strict';
var rapNewCaseStatusController = ['$scope', '$modal', 'alertService', 'rapnewcasestatusFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.CityUser = rapGlobalFactory.CityUser;
    //self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.SelectedCase;
    self.ActivityStatus = [];
    self.ActivityList = [];
    self.StatusList = [];
    self.clicked = function () {
        alert.Error("$watch");
        if (self.ActivityList.length == 0) {
            alert.Error("sadasd");
            return;
        }

        //At this point, newValue contains the new value of your persons array
        if ($(".custom-selectNEWCASE").length > 0) {
            alert.Error($(".custom-selectNEWCASE").length);

            $(".custom-selectNEWCASE").each(function () {
                alert.Error($(this).children('options').length);
                alert.Error($(this).children('option').length);
                var $this = $(this), numberOfOptions = $(this).children('option').length;
                var selected = $this.find("option[selected]");
                var placeholder = $this.data('placeholder');

                // Hides the select element
                $this.addClass('s-hidden');

                // Wrap the select element in a div
                $this.wrap('<div class="select"></div>');

                // Insert a styled div to sit over the top of the hidden select element
                $this.after('<div class="styledSelect"></div>');

                // Cache the styled div
                var $styledSelect = $this.next('div.styledSelect');

                // Show the first select option in the styled div
                if (selected.length > 0) {
                    if ($this.hasClass('show-values') && $(window).width() < 480) {
                        $styledSelect.text(selected.val());
                    } else {
                        $styledSelect.text(selected.text());//$this.children('option').eq(0).text());
                    }

                } else if (placeholder && placeholder.length > 0) {
                    $styledSelect.text($this.data('placeholder'));
                } else {
                    if ($this.hasClass('show-values') && $(window).width() < 480) {
                        $styledSelect.text($this.children('option').eq(0).val());
                    } else {
                        $styledSelect.text($this.children('option').eq(0).text());
                    }
                }

                // Insert an unordered list after the styled div and also cache the list
                var $list = $('<ul />', { 'class': 'options' }).insertAfter($styledSelect);

                // Insert a list item into the unordered list for each select option
                for (var i = 0; i < numberOfOptions; i++) {
                    $('<li />', { text: $this.children('option').eq(i).text(), rel: $this.children('option').eq(i).val() }).appendTo($list);
                }

                // Cache the list items
                var $listItems = $list.children('li');

                // Show the unordered list when the styled div is clicked (also hides it if the div is clicked again)
                $styledSelect.click(function (e) {
                    e.stopPropagation();
                    var $item = $(this);
                    var $list = $(this).next('ul.options').hide();

                    if ($item.hasClass('active')) {
                        $item.removeClass('active');
                        $list.stop().hide();
                    } else {
                        $item.addClass('active');
                        $list.stop().show();
                    }
                });

                // Hides the unordered list when a list item is clicked and updates the styled div to show the selected list item
                // Updates the select element to have the value of the equivalent option
                $listItems.click(function (e) {
                    e.stopPropagation();
                    if ($this.hasClass('show-values') && $(window).width() < 480) {
                        $styledSelect.text($(this).attr('rel')).removeClass('active');
                    } else {
                        $styledSelect.text($(this).text()).removeClass('active');
                    }
                    $this.val($(this).attr('rel'));
                    $list.hide();
                });

                // Hides the unordered list when clicking outside of it
                $(document).click(function () {
                    $styledSelect.removeClass('active');
                    $list.hide();
                });
            });
        }
    }
    var _getActivity = function () {
        return rapFactory.GetActivity().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.ActivityList = response.data;
            self.clicked();
        });
    }
    _getActivity();
    
    
    //var _getStatus = function () {
    //    return rapFactory.GetStatus(1).then(function (response) {
    //        if (!alert.checkResponse(response)) { return; }
    //        self.StatusList = response.data;
    //    });
    //}

    var _getEmptyActivityStatus = function () {
        return rapFactory.GetEmptyActivityStatus().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.ActivityStatus = response.data;
        });
    }

    
    //_getStatus();
    _getEmptyActivityStatus();
    
    

    self.ActivityChanged = function (model) {
        return rapFactory.GetStatus(model.ActivityID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.StatusList = response.data;
        });               
    }
    self.Submit = function (model, C_ID) {
        //TBD remove C_ID hardcoding
        model.EmployeeID = self.CityUser.EmployeeID;
        rapFactory.SaveNewActivityStatus(model, C_ID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $location.path("/staffdashboard");
        });        
    }
    self.Cancel = function (model, C_ID) {
        $location.path("/staffdashboard");
    }
}];
var rapNewCaseStatusController_resolve = {
    model: ['$route', 'alertService', 'rapnewcasestatusFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}