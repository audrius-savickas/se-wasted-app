export const formatDate = (string: string) => {
  const date = new Date(string)
  const year = date.getFullYear()
  const monthName = months[date.getMonth()]
  const day = date.getDate()
  const dayOfWeek = days[date.getDay()]
  return `${dayOfWeek}, ${day} ${monthName} ${year}`
}

export const formatTime = (string: string) => {
  const date = new Date(string)
  const hour = date.getHours()
  const minutes = date.getMinutes()
  return `${hour}:${minutes}`
}

export const timeAgo = (string: string) => {
  const date = new Date(string)

  const seconds = Math.floor(new Date().getTime() / 1000 - date.getTime() / 1000)
  let interval = seconds / 31536000

  if (interval >= 1) {
    if (interval === 1) return Math.floor(interval) + " years"
    return Math.floor(interval) + " year"
  }
  interval = seconds / 2592000
  if (interval >= 1) {
    if (interval === 1) return Math.floor(interval) + " months"
    return Math.floor(interval) + " month"
  }
  interval = seconds / 86400
  if (interval >= 1) {
    if (interval === 1) return Math.floor(interval) + " days"
    return Math.floor(interval) + " day"
  }
  interval = seconds / 3600
  if (interval >= 1) {
    if (interval === 1) return Math.floor(interval) + " hours"
    return Math.floor(interval) + " hour"
  }
  interval = seconds / 60
  if (interval >= 1) {
    if (interval === 1) return Math.floor(interval) + " minutes"
    return Math.floor(interval) + " minute"
  }
  if (interval === 1) return Math.floor(interval) + " seconds"
  return Math.floor(seconds) + " second"
}

const months = [
  "January",
  "February",
  "March",
  "April",
  "May",
  "June",
  "July",
  "August",
  "September",
  "October",
  "November",
  "December"
]

const days = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"]
