export default function dateFormater(date) {
    const newDate = date.split('T')[0];
    const time = date.split('T')[1].split('.')[0];

    const [hour, minutes] = time.split(':');

    const [year, month, day] = newDate.split('-');

    return `${day}.${month}.${year} at ${hour}:${minutes}`;
}
