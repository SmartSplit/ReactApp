var Table = React.createClass({
    render: function () {

        let tableHeaders = this.props.headers.map((header, i) => {
            return <th key={i}>{header}</th>;
        });

        return (
          <div className="table">
              <table className="table table-bordered table-striped">
                  <thead>
                    <tr>
                        {tableHeaders}
                    </tr>
                  </thead>
                  <tbody>
                    {this.props.users.map((user, i) => 
                        <tr>
                            {this.props.headers.map((header, i) => 
                                <td>{user[header]}</td>
                                )}
                            <td><RowButton user={user}/></td>
                        </tr>
                            )}
                  </tbody>
              </table>
        </div>
      );
    }
});