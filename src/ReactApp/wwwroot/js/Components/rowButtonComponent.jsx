var RowButton = React.createClass({
    render: function () {
        return (
            <div className="btn-group">
                  <button type="button" className="btn btn-success" href={this.props.user.showUrl}>Show</button>
                  <button type="button" className="btn btn-success dropdown-toggle" data-toggle="dropdown">
                    <span className="caret"></span>
                    <span className="sr-only">Toggle Dropdown</span>
                  </button>
                  <ul className="dropdown-menu" role="menu">
                    <li><a href="#">Edit</a></li>
                    <li><a href="#">Delete</a></li>
                  </ul>
                </div>
      );
    }
});