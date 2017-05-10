var UsersDisplay = React.createClass({
    render: function () {
        return (
          <div className="usersDisplay">
              <Table users={this.props[0].users} headers={['name', 'email']}>
              </Table>
        </div>
      );
    }
});