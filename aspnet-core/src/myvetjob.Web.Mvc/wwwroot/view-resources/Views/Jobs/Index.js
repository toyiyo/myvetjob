$(function () {
  "use strict";
  var page = 1; // Start from page 1
  var inProgress = false; // To prevent multiple simultaneous requests
  const pageSize = 10;

  updateFormWithSearchParams();

  $(window).scroll(function () {
    if (
      $(window).scrollTop() == $(document).height() - $(window).height() &&
      !inProgress
    ) {
      page++;
      loadMoreJobs();
    }
  });

  $("#jobFilterForm").on("submit", function (e) {
    e.preventDefault();
    page = 1; // Reset the page variable
    loadMoreJobs();
  });

  function updateFormWithSearchParams() {
    var searchParams = new URLSearchParams(window.location.search);
    searchParams.forEach(function (value, key) {
      var formElement = $("#jobFilterForm [name='" + key + "']");
      if (formElement) {
        formElement.val(value);
      }
    });
  }

  function buildUrl(formSelector, page, pageSize) {
    var formData = $(formSelector).serialize();
    var dataObject = new URLSearchParams(formData);
  
    dataObject.append("skipCount", (page - 1) * pageSize);
    dataObject.append("maxResultCount", pageSize);
  
    return $(formSelector).attr("action") + "?" + dataObject.toString();
  }

  function loadMoreJobs() {
    inProgress = true;
    // Clear existing jobs if this is the first page
    if (page === 1) {
      $(".job-openings").empty();
    }
    abp.ui.setBusy($(".job-openings")); 


    var newUrl = buildUrl("#jobFilterForm", page, pageSize); 
  
    $.ajax({
      url: newUrl,
      //data: dataObject.toString(),
      success: function (data) {
        if (data.trim()) {
          // Insert the HTML returned by the server into the existing list of jobs
          $(".job-openings").append(data);
          //update the URL with the new page number, only if new data was loaded
          window.history.pushState({ path: newUrl }, '', newUrl);
        } else {
          console.log("No more jobs to load.");
          page--; // Decrement page since no new data was loaded
        }
        inProgress = false;
      },
      complete: function () {
        abp.ui.clearBusy($(".job-openings")); // Clear the busy indicator
        inProgress = false;
      },
    });
  }
});
