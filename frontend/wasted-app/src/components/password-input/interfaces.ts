export interface PasswordInputProps {
  label?: string
  hint?: string
  showPassword: boolean
  password: string
  setPassword: (input: string) => void
  setPasswordValid: (valid: boolean) => void
  setShowPassword: (show: boolean) => void
}
