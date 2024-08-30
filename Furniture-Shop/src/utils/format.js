export const formattedDate = (dateTimeString) => {
    // Convert the string to a Date object
    const dateObj = new Date(dateTimeString);

    // Format the date and time as desired
    const formattedDate = dateObj.toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'long',
        day: '2-digit'
    });

    const formattedTime = dateObj.toLocaleTimeString('en-US', {
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
        hour12: true // For 12-hour format, set to false for 24-hour format
    });
    return `${formattedDate} ${formattedTime}`
}

export const formattedPrice = (price) => {
    return new Intl.NumberFormat().format(price);
};