export interface PasswordInputProps {
  showPassword: boolean
  password: string
  setPassword: (input: string) => void
  setPasswordValid: (valid: boolean) => void
  setShowPassword: (show: boolean) => void
}
