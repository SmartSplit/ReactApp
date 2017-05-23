function DisplayUsers(...UserDTO){
    ReactDOM.render(
      <UsersDisplay {... UserDTO}/>,
      document.getElementById('content')
    );
}