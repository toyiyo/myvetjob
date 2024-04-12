$(function () {
  "use strict";
  var page = 1; // Start from page 1
  var inProgress = false; // To prevent multiple simultaneous requests
  var _jobService = abp.services.app.job; // Your job service

  $(window).scroll(function () {
    if (
      $(window).scrollTop() == $(document).height() - $(window).height() &&
      !inProgress
    ) {
      page++;
      loadMoreJobs();
    }
  });

  function loadMoreJobs() {
    inProgress = true;
    abp.ui.setBusy($(".job-openings")); // Set the card body as busy during the request

    $.ajax({
      url: "/Jobs/GetActiveJobs",
      data: {
        skipCount: (page - 1) * 10,
        maxResultCount: 10, // Replace 10 with your actual page size
      },
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
