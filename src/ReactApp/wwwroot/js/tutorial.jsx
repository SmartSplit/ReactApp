var CommentBox = React.createClass({
    render: function() {
        return (
          <div className="commentBox">
            Hello, world! I am a CommentBox. {this.props[0].users[0].name}
          </div>
      );
    }
});

function DisplayUsers(...UserDTO){
    ReactDOM.render(
      <CommentBox {... UserDTO}/>,
      document.getElementById('content')
    );
}