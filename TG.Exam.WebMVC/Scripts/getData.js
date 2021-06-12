let getAllAsync = $('#getAllAsync');

getAllAsync.click(async ()=>{
    let response = await fetch("/api/UsersApi/Get");
    const users = await response.json();
    let usersData = $('#usersData');
    usersData.empty();

    const usersRows = users.map((user, i) =>
        `<tr>
            <td>${i + 1}</td>
            <td>${user.FirstName}</td>
            <td>${user.LastName}</td>
            <td>${user.Age + 10}</td>
            <td>Async</td>
         </tr>`).join('');
    usersData.append(usersRows);
});