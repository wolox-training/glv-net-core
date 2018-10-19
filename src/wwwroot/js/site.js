$(function () 
{
    $("#AddCommentButton").click(function ()
    {
      var textComment = $('#textForm').val();
      var movieIdComment = $('#movieIdForm').val();
      $.ajax({  
        type: 'POST',  
        url: '../api/v1/CommentsAPI/AddComment',  
        data: { idMovie: movieIdComment, commentText: textComment },  
        success: function (response) {
          $("#commentsBlock").append(`<tr><td>${response.comment}</td></tr>`);
          $("#textForm").val('');
        },
        error: function (response){  
            alert('Sorry: Something Wrong');  
        },
      });
    });
});
