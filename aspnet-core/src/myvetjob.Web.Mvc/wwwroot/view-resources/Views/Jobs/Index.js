$(function () {
  "use strict";
  var page = 1; // Start from page 1
  var inProgress = false; // To prevent multiple simultaneous requests

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

    $.ajax({
      url: "/Jobs/Index",
      data: dataObject.toString(),
      success: function (data) {
        // Insert the HTML returned by the server into the existing list of jobs
        $(".job-openings").append(data);
        inProgress = false;
      },
      complete: function () {
        abp.ui.clearBusy($(".job-openings")); // Clear the busy indicator
        inProgress = false;
      },
    });
  }
});
