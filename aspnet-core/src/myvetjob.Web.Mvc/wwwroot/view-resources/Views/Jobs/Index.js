$(function () {
  "use strict";
  var page = 1; // Start from page 1
  var inProgress = false; // To prevent multiple simultaneous requests
  var searchParams = new URLSearchParams(window.location.search);
  searchParams.forEach(function(value, key) {
    var formElement = $("#jobFilterForm [name='" + key + "']");
    if (formElement) {
      formElement.val(value);
    }
  });

  // Optionally, trigger the form submission to load the initial set of jobs based on URL parameters
  //$("#jobFilterForm").submit();

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
    abp.ui.setBusy($(".job-openings"));
    var form = $(this);
    var url = form.attr("action");
    var serializedData = form.serialize();

    // Update the URL with form data as query parameters
    var newUrl = url + "?" + serializedData;
    window.history.pushState({ path: newUrl }, '', newUrl);


    page = 1; // Reset the page variable

    $.ajax({
      type: "GET",
      url: url,
      data: serializedData, // This will include all the form fields in the AJAX request
      success: function (data) {
        // Insert the HTML returned by the server into the existing list of jobs
        $(".job-openings").html(data);
      },
      complete: function () {
        abp.ui.clearBusy($(".job-openings"));
      },
    });
  });

  function loadMoreJobs() {
    inProgress = true;
    abp.ui.setBusy($(".job-openings")); // Set the card body as busy during the request

    var formData = $("#jobFilterForm").serialize();
    var dataObject = new URLSearchParams(formData);

    dataObject.append("skipCount", (page - 1) * 10);
    dataObject.append("maxResultCount", 10); // Replace 10 with your actual page size

    var newUrl = $("#jobFilterForm").attr("action") + "?" + dataObject.toString();
  

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
