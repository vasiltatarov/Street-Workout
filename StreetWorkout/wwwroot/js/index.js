$('#show-stats-btn').on('click', ev => {
    $.get('/api/statistics', (data) => {
        $('#total-trainers').text(data.totalTrainers + ' Trainers');
        $('#total-enthusiasts').text(data.totalEnthusiasts + ' Enthusiasts');
        $('#total-workouts').text(data.totalWorkouts + ' Workouts');

        $('#show-stats').removeClass('d-none');
        $('#show-stats-btn').hide();
    });
})
