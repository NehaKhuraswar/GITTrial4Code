var $ = jQuery;
if(window.location.hostname==='localhost') {
  siteURL = "http://localhost/RAPOakland";
} else {
  siteURL = "http://fssites.wpengine.com/RAPOakland";
}
var tooltips;

( function( $ ) {
  $.fn.equalizeHeights = function(){
    return this.height( Math.max.apply(this, $(this).map(function(i,e){return $(e).height()}).get() ) )
  }
    
  function scrollToElement(selector, time, verticalOffset, callback) {
    if(selector.length > 0) {
      time = typeof(time) != 'undefined' ? time : 500;
      verticalOffset = typeof(verticalOffset) != 'undefined' ? verticalOffset : 0;
      element = $(selector);
      offset = element.offset();
      offsetTop = offset.top + verticalOffset;
      t = ($(window).scrollTop() - offsetTop);
      if (t <= 0) t *= -1;
      t = parseInt(t * .5);
      if (t < time) t=time;
      if (t > 1500) t=1500;

      $('html, body').animate({
        scrollTop: offsetTop
      }, t, '', callback);
    }
  } 
  
  function equalizeHeight() {
    if($('.content-for-consumers').length > 0) {
      $('.content-for-consumers, .content-for-businesses').css({'height': 'auto'});
      if($(window).width() > 992 ) {
        $('.content-for-consumers, .content-for-businesses').equalizeHeights();
      }
    }
  }
  
  
  function resizeWindow() {
    equalizeHeight();
  }
  function loadWindow() {
    equalizeHeight();
  }
  
  function toogleFields(el, typ, prop){
    if(el.length > 0){
      if(typ === 'show') {
        (prop) ? el.show() : el.hide();
      } else {
        (prop) ? el.hide() : el.show();
      }
    }
  }
  
  function toogleChkFields(el, prop){
    if(el.length > 0){
      (prop) ? el.show() : el.hide();
    }
  }
  
  
  function setApplicationSteps() {
    var path = window.location.href;

    $(".application-steps li").each(function () {
      var $item = $(this);
      var $link = $('a', $item);
      var href = $link.attr('href');
      
      if (path.match(href)){
        $item.addClass('current');
        $item.prevAll().removeClass('current').addClass('active');
      }
    });
  }
  
  //Close Toggle Menu
  function closeToggleMenu(){
    var $pageSlide = $('.pageSlide');
    $pageSlide.stop().animate({'right': '-100%' }, 500);
    $pageSlide.removeClass('open');
    $('.menu-toggle').removeClass('open');
  }
  
  
  
  ( function() {
    $(window).load(loadWindow);
    $(window).resize(resizeWindow);
    
    
    //Set Sidebar Steps Nav
  //  setApplicationSteps();
    
    //Application Steps - Mobile Toggle
    if($('.application-steps-toggle').length > 0){
      $('.application-steps-toggle .steps-toggle').click(function(){
        var $btnToggle = $(this);
        var $applicationSteps = $('.application-steps');
        $btnToggle.toggleClass('collapsed');
        $applicationSteps.stop().slideToggle();
      });
    }
    
    
    //Toggle Menu
    if($('.menu-toggle').length > 0){
      var $navigation = $('.navigation-wrapper');
      var mobNavInCache = 0;
      var $pageSlide = $('.pageSlide');
      var $toggleContainer;

      $('.menu-toggle').on('click', function(e){
        e.preventDefault();
        e.stopPropagation();

        if( $pageSlide.hasClass('open') ){
          $pageSlide.stop().animate({'right': '-100%' }, 500);
        } else {
          $pageSlide.stop().animate({ 'right': '0' }, 500);
        }

        $pageSlide.toggleClass('open');

        //Appened Navigation Links
        if(mobNavInCache===0){
          $toggleContainer = $('<div class="toggle-container">'+$navigation.html()+'</div>');
          $pageSlide.html($toggleContainer);
          mobNavInCache = 1;
        }
        
        //Toggle Menu Class
        $(this).toggleClass('open');
        
        
        //Mobile Nav Accordian
        if($pageSlide.hasClass('open')){
          $('li.menu-item-has-children', $pageSlide).each(function(){
            var $item = $(this);
            var $btnToggle = $('<a href="#" class="btn-toggle"></a>');
            $item.append($btnToggle);
            
            $btnToggle.on('click', function(e){
              e.preventDefault();
              var $subMenu = $('.sub-menu', $item);
              $('.menu-item-has-children').not($item).removeClass('active');
              $subMenu.slideToggle();
              $item.toggleClass('active');
            });
          });
        }
      });

//      $(document).on('click', function closeMenu(e){
//        if( !$(e.target).hasClass('open') ) {
//          closeToggleMenu();
//        }
//      });

      $('.close-menu').on('click', function(){
        closeToggleMenu();
      });
    }
    
    
    //Toggle - Search and Choose Language
    if($('.account-details .toggle-link').length > 0){
      $('.account-details .toggle-link').each(function(){
        var $item = $(this);
        var $itemToggle = $('.'+$item.data('toggle'));
        
        if($itemToggle.length > 0){
          $item.on('click', function(e){
            e.preventDefault();
            $('.account-details .toggle-link').not($item).removeClass('active');
            $('.navigation-wrapper .toggle-nav-element').not($itemToggle).removeClass('show');
            
            $item.toggleClass('active');
            $itemToggle.toggleClass('show');
          });
        }
        
      });
    }
    
    
    
    if($('.accordion').length > 0) {
      $('.accordion .item').each(function() {
        var $container = $(this);
        var $itemHeader = $('.item-header', $container);
        var $itemContent = $('.item-content', $container);
        var $itemToggle;
        
        if($container.hasClass('expanded')){
          $itemContent.stop().slideToggle();
        }
        
        if($('.btn-toggle', $itemHeader).length > 0){
          $itemToggle = $('.btn-toggle', $itemHeader);
        } else {
          $itemToggle = $itemHeader;
        }
        
        $itemToggle.click(function(e) {
          e.preventDefault();
          $itemContent.stop().slideToggle();
          $container.toggleClass('expanded');
        });
      });
    }
    
    //Toogle Content
    if($('.item-toggle').length > 0) {
      $('.item-toggle').each(function() {
        var $item = $(this);
        var $itemHeader = $('.item-header', $item);
        var $itemContent = $('.item-content', $item);
        var $itemToggle;
        
        if($('.btn-toggle', $itemHeader).length > 0){
          $itemToggle = $('.btn-toggle', $itemHeader);
          
          $itemToggle.click(function(e) {
            e.preventDefault();
            $itemContent.stop().slideToggle();
            $item.toggleClass('expanded');
          });
        }
      });
    }
    
    //Case in Progress form - Related Info Toogle
    if($('.info-toggle').length > 0) {
      $('.info-toggle').each(function() {
        var $item = $(this);
        var $itemHeader = $('.info-header', $item);
        var $itemContent = $('.info-content', $item);
        var $itemToggle;
        
        if($('.btn-toggle', $itemHeader).length > 0){
          $itemToggle = $('.btn-toggle', $itemHeader);
          
          $itemToggle.click(function(e) {
            e.preventDefault();
            $itemContent.stop().slideToggle();
            $item.toggleClass('expanded');
          });
        }
      });
    }
    
    
    if($('.docs-list .item').length > 0){
      $('.docs-list .item').each(function() {
        var $item = $(this);
        var $itemHeader = $('.item-header', $item);
        var $itemDelete = $('.item-delete', $item);
        var $btnDelete = $('.btn-delete', $itemHeader);
        
        //File Delete Button Click
        $btnDelete.on('click', function(e){
          e.preventDefault();
          $btnDelete.addClass('hidden');
          
          if($('.item-delete', $item).length > 0){
            if($('.item-delete', $item).hasClass('hidden')){
              $('.item-delete', $item).toggleClass('hidden');
            }
          } else {
            var $deleteBox = $('<div class="item-delete hidden">');
            $deleteBox.append('<strong>Are you sure that you want to delete this file?</strong>');
            $deleteBox.append('<div class="action-links"><a href="#" class="btn-link btn-delete">Delete</a> <a href="#" class="btn-link btn-cancel">Cancel</a></div>');
            $item.append($deleteBox);
            $deleteBox.removeClass('hidden');
          }
          
          
          //Cancel Button Click
          if($('.item-delete .btn-cancel', $item).length > 0){
            $('.item-delete .btn-cancel', $item).on('click', function(e){
              e.preventDefault();
              var $btnCancel = $(this);
              var $item = $btnCancel.parents('.item');
              var $itemDelete = $('.item-delete', $item);
              $itemDelete.addClass('hidden');
              $btnDelete.removeClass('hidden');
            });
          }
        });
      });
    }
    
    

    if($('input[data-toggle]').length > 0) {
      $('input[data-toggle]').each(function(){
        var $item = $(this);
        var toggleType = $item.data('toggle');
        var $toogleObj = $('#'+$item.data('toggle-id'));
        
        toogleFields($toogleObj, toggleType, $item.prop('checked'));
        
        $item.change(function (e) {
          toogleFields($toogleObj, toggleType, $item.prop('checked'));
        });
      });
    }
    
    
    if($('input[data-chktoggle]').length > 0) {
      $('input[data-chktoggle]').each(function(){
        var $item = $(this);
        var $itemParent = $item.parents('.checkbox-wrapper');
        var $ralatedInfo = $('.related-info', $itemParent);
        
        toogleChkFields($ralatedInfo, $item.prop('checked'));
        
        $item.change(function (e) {
          toogleChkFields($ralatedInfo, $item.prop('checked'));
        });
      });
    }
    
    
    //Icon Info - Toggle Box (on i icon click)
    if($('.icon-info-toggle').length > 0) {
      $('.icon-info-toggle').each(function() {
        var $item = $(this);
        var $itemParent = $item.parents('.field-group');
        var $itemContent = $('.info-box-toggle', $itemParent);
        
        
        if($itemContent.length > 0){
          $item.on('click', function(e) {
            e.preventDefault();
            $itemContent.fadeIn();
            $itemContent.toggleClass('hidden');
          });
          
          $('.btn-close', $itemContent).on('click', function(e) {
            e.preventDefault();
            $itemContent.fadeOut();
            $itemContent.toggleClass('hidden');
          });
        }
      });
    }
   
    
//    if($('#frmCreateAccount').length > 0) {
//      $("#phone_number").mask("(999) 999-9999");
//      $("#ss_number").mask("999-99-9999");
//      $("#ar_number").mask("999-999-9999");
//    }

    tooltipFileUrl = siteURL+'/js/tooltips.json';
    $.getJSON(tooltipFileUrl, function(results) {
      if(results){
        tooltips = results;
        $('[data-toggle="tooltip"]').each(function() {
          var $item = $(this);
          var $itemText = tooltips[$item.data('tooltip-id')];
          if(tooltips[$item.data('tooltip-id')]) {
            $item.tooltip({
              title: $itemText,
              html: true,
              placement: 'top'
            });
          }
        });
      } else {
        alert('Tooltip data not found');
      }
    });
    
    
    
    $('.application-form').each(function() {
      var $form = $(this);
      var err_msg;
      
      $form.submit(function() {
        isValid = true;
        var $formWrapper = $(this).parents('.application-step');
        var $alert = $('.alert:eq(0)', $formWrapper);
        err_msg = '';
        $('.input-error').removeClass('input-error');
        $('.has-radio-error').removeClass('has-radio-error');
        $('.has-checkbox-error').removeClass('has-checkbox-error');
        
        if($alert.length > 0) {
          $alert.removeClass('alert-success alert-info alert-warning');
          $alert.empty().addClass('hidden');
        }

        /* Global validation for required radio fields*/
        $('.form-group-radio.required', $form).each(function() {
          var $parent = $(this);
          if($parent.hasClass('hidden') || $parent.parents('.hidden').length > 0) {
            // do nothing if field is hidden
          } else {
            if($('input', $parent).length > 0) {
              var inputName = $('input:eq(0)', $parent).attr('name');
              if(!($('input[name='+inputName+']:checked').val())) {
                $parent.addClass('has-radio-error');
                err_msg += '<strong><a href="javascript:void();" data-scrollto="'+$parent.attr('id')+'">'+$parent.data('error')+'</a></strong>  is a required field<br />';
                isValid=false;
              }
            }
          }

          if($parent.hasClass('has-radio-error')){
            $('.input-error-msg', $parent).empty().append('This is a required field').removeClass('hidden');
          } else {
            $('.input-error-msg', $parent).empty().addClass('hidden');
          }
        });
        
        /* Global validation for required checkbox fields */
        $('.form-group-checkbox.required', $form).each(function() {
          var $parent = $(this);
          if($parent.hasClass('hidden') || $parent.parents('.hidden').length > 0) {
            // do nothing if field is hidden
          } else {
            if($('input', $parent).length > 0) {
              var inputName = $('input:eq(0)', $parent).attr('name');
              if(!($('input[name='+inputName+']:checked').val())) {
                $parent.addClass('has-checkbox-error');
                err_msg += '<strong><a href="javascript:void();" data-scrollto="'+$parent.attr('id')+'">'+$parent.data('error')+'</a></strong>  is a required field<br />';
                isValid=false;
              }
            }
          }

          if($parent.hasClass('has-checkbox-error')){
            $('.input-error-msg', $parent).empty().append('This is a required field').removeClass('hidden');
          } else {
            $('.input-error-msg', $parent).empty().addClass('hidden');
          }
        });
        
        $('.field-input.required', $form).each(function() {
          var $fieldGroup = $(this).parents('.field-group');
          if($(this).val().replace(/^\s*|\s*$/g,"")=="") {
            $(this).addClass('input-error');
            isValid=false;

            err_msg += '<strong><a href="javascript:void();" data-id="'+$(this).attr('id')+'">'+$(this).data('error')+'</a></strong>  is a required field<br />';

            if($('.input-error-msg', $fieldGroup).length > 0){
              $('.input-error-msg', $fieldGroup).empty().append('This is a required field').removeClass('hidden');
            }
          } else {
            if($('.input-error-msg', $fieldGroup).length > 0){
              $('.input-error-msg', $fieldGroup).empty().addClass('hidden');
            }
          }
        });

        if(isValid) {
          $('.field-input.email', $form).each(function() {
            var $fieldGroup = $(this).parents('.field-group');
            
            if($(this).val().replace(/^\s*|\s*$/g,"")=="") return;
            if(isValid && emailfilter.test($(this).val())==false) {
              $(this).addClass('input-error');
              isValid = false;
              
              if($('.input-error-msg', $fieldGroup).length > 0){
                $('.input-error-msg', $fieldGroup).empty().append('This is a required field').removeClass('hidden');
              }
            } else {
              if($('.input-error-msg', $fieldGroup).length > 0){
                $('.input-error-msg', $fieldGroup).empty().addClass('hidden');
              }
            }
          });
        }
        
        if(!(isValid)) {
          $alert.addClass('alert-error');
          $alert.html(err_msg).removeClass('hidden');
          scrollToElement($alert, 800, -100);
          
          if($('a', $alert).length > 0){
            $('a', $alert).each(function() {
              var $item = $(this);
              $item.click(function(e) {
                e.preventDefault();
                var scrollTo = $('#'+$item.data('scrollto'));
                var focusOn = $('#'+$item.data('id'));
                
                if(focusOn.length > 0) {
                  $('#'+$item.data('id')).focus();
                }
                if(scrollTo.length > 0) {
                  scrollToElement(scrollTo, 800, -60); 
                }
                
              });
            });
          }
          
        }
        return isValid;
      });
        
//      if(isValid) {
//        if($('#agree_terms', $form).prop('checked')) {
//          //
//        } else {
//          err_msg = "Please agree to the challenge rules in order to Submit.";
//          isValid = false;
//        }
//      }
    });
    
    //Form Validation error field scroll and focus on field link click
    if($('.application-form-wrapper .alert a').length > 0){
      $('.application-form-wrapper .alert a').on('click', function(e) {
        e.preventDefault();
        var $item = $(this);
        var scrollTo = $('#'+$item.data('scrollto'));
        var focusOn = $('#'+$item.data('id'));

        if(focusOn.length > 0) {
          $('#'+$item.data('id')).focus();
        }
        if(scrollTo.length > 0) {
          scrollToElement(scrollTo, 800, -60); 
        }
      });
    }
    
    
    //if(window.location.hash) {
    //  hash = window.location.hash.replace(/^.*#/, ''); 
    //  if(hash != ''){
    //    if($("."+hash).length > 0){
    //      scrollToElement($("."+hash), 800, 0);
    //    }
    //    if($("#"+hash).length > 0){
    //      scrollToElement($("#"+hash), 800, 0);
    //    }
    //  }
    //}
    
    if($('.add-upload-document').length > 0) {
      $('.add-upload-document').click(function(e) {
        e.preventDefault();
        if($('.upload-document.hidden').length > 0) {
          $('.upload-document.hidden:eq(0)').toggleClass('hidden');
        }

        if($('.upload-document.hidden').length <= 0) {
          $(this).toggleClass('hidden-xs-up');
        }
      });
    }
    
    if($('#discription').length > 0) {
      $('#discription').textcounter({max: 1500, countDown: true, countDownText: 'characters remaining'});
    }
    
    if($('#complaint_discription').length > 0) {
      $('#complaint_discription').textcounter({max: 1500, countDown: true, countDownText: 'characters remaining'});
    }
    if($('#satisfy_complaint').length > 0) {
      $('#satisfy_complaint').textcounter({max: 1500, countDown: true, countDownText: 'characters remaining'});
    }
    
    if($('.upload-document').length > 0) {
      $('.upload-document').each(function() {
        $('textarea', $(this)).textcounter({max: 500, countDown: true, countDownText: 'characters remaining'});
      });
    }
    
    if($( ".field-datepicker" ).length > 0) {
      $( ".field-datepicker" ).datepicker({
        dateFormat: "m/d/y",
        dayNamesMin: ['S', 'M', 'T', 'W', 'TH', 'F', 'S'],
        showOtherMonths: true,
        selectOtherMonths: false,
        maxDate: new Date()
      });
    
    }
    
    
    //Implement Custom Select Box
    if($(".custom-select").length > 0){
      $(".custom-select").each(function () {
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
        if(selected.length > 0) {
          if($this.hasClass('show-values') && $(window).width() < 480) {
            $styledSelect.text(selected.val());
          } else {
            $styledSelect.text(selected.text());//$this.children('option').eq(0).text());
          }

        } else if (placeholder && placeholder.length > 0){
          $styledSelect.text($this.data('placeholder'));
        } else {
          if($this.hasClass('show-values') && $(window).width() < 480) {
            $styledSelect.text($this.children('option').eq(0).val());
          } else {
            $styledSelect.text($this.children('option').eq(0).text());
          }
        }

        // Insert an unordered list after the styled div and also cache the list
        var $list = $('<ul />', {'class': 'options'}).insertAfter($styledSelect);

        // Insert a list item into the unordered list for each select option
        for (var i = 0; i < numberOfOptions; i++) {
          $('<li />', {text: $this.children('option').eq(i).text(), rel: $this.children('option').eq(i).val()}).appendTo($list);
        }

        // Cache the list items
        var $listItems = $list.children('li');

        // Show the unordered list when the styled div is clicked (also hides it if the div is clicked again)
        $styledSelect.on('click', function (e) {
          e.stopPropagation();
          var $item = $(this);
          var $list = $(this).next('ul.options').hide();

          if($item.hasClass('active')) {
            $item.removeClass('active');
            $list.stop().hide();
          } else {
            $item.addClass('active');
            $list.stop().show();
          }
        });

        // Hides the unordered list when a list item is clicked and updates the styled div to show the selected list item
        // Updates the select element to have the value of the equivalent option
        $listItems.on('click', function (e) {
          e.stopPropagation();
          if($this.hasClass('show-values') && $(window).width() < 480) {
            $styledSelect.text($(this).attr('rel')).removeClass('active');
          } else {
            $styledSelect.text($(this).text()).removeClass('active');
          }
          $this.val($(this).attr('rel'));
          $list.hide();
        });

        // Hides the unordered list when clicking outside of it
        $(document).on('click', function () {
           $styledSelect.removeClass('active');
           $list.hide();
        });
      });
    }
    
    
  } )();
  
} )( jQuery );