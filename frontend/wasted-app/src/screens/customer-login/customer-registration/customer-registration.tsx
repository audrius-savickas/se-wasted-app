import React, {useEffect, useState} from "react"
import {Alert, ScrollView, StyleSheet} from "react-native"
import {Navigation} from "react-native-navigation"
import {Button, Card, Colors, Incubator, Text, View} from "react-native-ui-lib"
import {registerCustomer} from "../../../api/customer"
import {PasswordInput} from "../../../components/password-input"
import {CustomerRegistrationProps} from "./interfaces"

export const CustomerRegistration = ({componentId}: CustomerRegistrationProps) => {
  const [firstName, setFirstName] = useState("")
  const [lastName, setLastName] = useState("")
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [error, setError] = useState("")

  const [firstNameValid, setFirstNameValid] = useState(true)
  const [lastNameValid, setLastNameValid] = useState(true)
  const [emailValid, setEmailValid] = useState(true)
  const [passwordValid, setPasswordValid] = useState(true)
  const [confirmPasswordValid, setConfirmPasswordValid] = useState(true)

  const [showPassword, setShowPassword] = useState(false)
  const [showConfirmPassword, setShowConfirmPassword] = useState(false)

  const valid = firstNameValid && lastNameValid && emailValid && passwordValid && confirmPasswordValid

  const finishRegistration = async () => {
    if (valid) {
      if (password !== confirmPassword) {
        setError("Passwords don't match")
      } else {
        const userId = await registerCustomer({credentials: {email, password}, firstName, lastName})
        if (userId) {
          setError("")
          Alert.alert("Registered succesfully!", "Please check your inbox for confirmation email.", [{text: "OK"}])
          Navigation.pop(componentId)
        } else {
          setError("There is already an account registered on this email.")
        }
      }
    } else {
      setError("Please check your input fields.")
    }
  }

  useEffect(() => {
    if (valid) {
      setError("")
    }
  }, [valid])

  return (
    <ScrollView>
      <View flexG center marginB-s10 marginT-s8>
        <View br30 margin-s4 marginB-s8 bg-grey70 padding-s4>
          <Text text70L>Please fill these fields in order to register your account!{`\n* - required fields`}</Text>
        </View>
        <View centerV width={340}>
          <Incubator.TextField
            validateOnChange
            enableErrors
            marginB-s2
            autoCapitalize="none"
            hint="Your first name"
            fieldStyle={styles.withUnderline}
            label="First name*"
            validate="required"
            validationMessage="First name is required"
            value={firstName}
            onChangeText={setFirstName}
            onChangeValidity={setFirstNameValid}
          />
          <Incubator.TextField
            validateOnChange
            enableErrors
            marginB-s6
            autoCapitalize="none"
            hint="Your last name"
            fieldStyle={styles.withUnderline}
            label="Last name*"
            validate="required"
            validationMessage="Last name is required"
            value={lastName}
            onChangeText={setLastName}
            onChangeValidity={setLastNameValid}
          />
          <Incubator.TextField
            validateOnChange
            enableErrors
            marginB-s6
            autoCapitalize="none"
            hint="Your account's email"
            fieldStyle={styles.withUnderline}
            label="Email address*"
            validate={["required", "email"]}
            validationMessage={["Email is required", "Email is invalid"]}
            value={email}
            onChangeText={setEmail}
            onChangeValidity={setEmailValid}
          />
          <PasswordInput
            label="Password*"
            password={password}
            setPassword={setPassword}
            showPassword={showPassword}
            setShowPassword={setShowPassword}
            setPasswordValid={setPasswordValid}
          />
          <View marginB-s10>
            <PasswordInput
              label="Confirm password*"
              hint="Your account's repeated password"
              password={confirmPassword}
              setPassword={setConfirmPassword}
              showPassword={showConfirmPassword}
              setShowPassword={setShowConfirmPassword}
              setPasswordValid={setConfirmPasswordValid}
            />
            <Card padding-s3 backgroundColor={Colors.grey70}>
              <Text text70L>Password should contain:</Text>
              <Text
                text80L
              >{`  ∙ at least 8 characters\n  ∙ 1 or more capital letters\n  ∙ 1 digit\n  ∙ 1 special character`}</Text>
            </Card>
          </View>
        </View>
        <Button bg-blue40 label="Register" onPress={finishRegistration} />
        <View marginT-s2 style={{opacity: error ? 100 : 0}}>
          <Text center text70L red10 style={styles.error}>
            {error}
          </Text>
        </View>
      </View>
    </ScrollView>
  )
}

const styles = StyleSheet.create({
  withUnderline: {
    borderBottomWidth: 1,
    borderColor: Colors.blue60,
    paddingBottom: 4
  },
  error: {position: "absolute", alignSelf: "center", width: "85%"},
  map: {
    height: 200
  }
})
