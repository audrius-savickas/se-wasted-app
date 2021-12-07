import moment from "moment"

export const formatDate = (string: string) => {
  const date = new Date(string)
  const year = date.getFullYear()
  const monthName = months[date.getMonth()]
  const day = date.getDate()
  const dayOfWeek = days[date.getDay()]
  return `${dayOfWeek}, ${day} ${monthName} ${year}`
}

export const formatTime = (string: string) => {
  const date = new Date(moment(string).toISOString())
  const hour = date.getHours()
  const minutes = date.getMinutes()

  const hourString = hour < 10 ? `0${hour}` : hour.toString()
  const minuteString = minutes < 10 ? `0${minutes}` : minutes.toString()

  return `${hourString}:${minuteString}`
}

export const timeAgoFull = (string: string) => {
  moment.updateLocale("en", {
    relativeTime: {
      future: "in %s",
      past: "%s",
      s: "1 second ago",
      ss: "%s seconds ago",
      m: "1 minute ago",
      mm: "%d minutes ago",
      h: "1 hour ago",
      hh: "%d hours ago",
      d: "1 day ago",
      dd: "%d days ago",
      M: "1 month ago",
      MM: "%d months ago",
      y: "1 year ago",
      yy: "%d years ago"
    }
  })

  const date = new Date(string)
  return moment(date).fromNow()
}

export const timeAgo = (string: string) => {
  moment.updateLocale("en", {
    relativeTime: {
      future: "in %s",
      past: "%s",
      s: "1 sec",
      ss: "%s secs",
      m: "1 min",
      mm: "%d mins",
      h: "1 h",
      hh: "%d h",
      d: "1 day",
      dd: "%d days",
      M: "1 month",
      MM: "%d months",
      y: "1 year",
      yy: "%d years"
    }
  })

  const date = new Date(string)
  return moment(date).fromNow()
}

export const convertMinsToHrsMins = (mins: number) => {
  const h = Math.floor(mins / 60)
  const m = mins % 60
  if (h) {
    if (m) {
      return `${h} h, ${m} min`
    }
    return `${h} h`
  }
  return `${m} min`
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
