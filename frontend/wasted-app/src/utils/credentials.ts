export const convertPassword = (password: string) => {
  return password
    .split("")
    .map(() => "*")
    .join("")
}
