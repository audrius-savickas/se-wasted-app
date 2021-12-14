import {GoogleSignin, GoogleSigninButton, statusCodes} from "@react-native-google-signin/google-signin"
import React, {useEffect, useState} from "react"
import {StyleSheet} from "react-native"
import {Button, Colors, Incubator, Text, View} from "react-native-ui-lib"
import {loginCustomer} from "../../api/customer"
import {PasswordInput} from "../../components/password-input"
import {useAuthentication} from "../../hooks/use-authentication"
import {useCustomer} from "../../hooks/use-customer"
import {navigateToCustomerRegistration, setUserRoot} from "../../services/navigation"
import {CustomerLoginProps} from "./interfaces"

export const CustomerLogin = ({componentId}: CustomerLoginProps) => {
  const {setCustomerId} = useCustomer()
  const {setUser} = useAuthentication()

  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const [emailValid, setEmailValid] = useState(false)
  const [passwordValid, setPasswordValid] = useState(false)
  const [error, setError] = useState("")
  const [showPassword, setShowPassword] = useState(false)

  const valid = passwordValid && emailValid

  const loginGoogle = async () => {
    try {
      await GoogleSignin.hasPlayServices()
      const userInfo = await GoogleSignin.signIn()

      setUser(userInfo)
      setEmail(userInfo.user.email)
      setError("Please input password")
      setEmailValid(true)
    } catch (error: any) {
      if (error.code === statusCodes.SIGN_IN_CANCELLED) {
        // user cancelled the login flow
      } else if (error.code === statusCodes.IN_PROGRESS) {
        // operation (e.g. sign in) is in progress already
      } else if (error.code === statusCodes.PLAY_SERVICES_NOT_AVAILABLE) {
        // play services not available or outdated
      } else {
        // some other error happened
      }
    }
  }

  const loginEmail = async () => {
    if (__DEV__ && email === "DEV") {
      setCustomerId({customerId: "7714624a-dc5b-4bbc-9290-53718129a962"})
      setUserRoot()
    }
    if (valid) {
      const response = await loginCustomer({credentials: {email, password}})
      if (response) {
        setCustomerId({customerId: response.customerId})
        setUserRoot()
      } else {
        setError("Login failed. We haven't found a registered user account with these credentials.")
      }
    } else {
      setError("Please check your input fields.")
    }
  }

  const navigateToRegistration = () => {
    navigateToCustomerRegistration(componentId)
  }

  useEffect(() => {
    if (valid) {
      setError("")
    }
  }, [valid])

  useEffect(() => {
    if (password) {
      setPasswordValid(true)
    } else {
      setPasswordValid(false)
    }
  }, [password])

  return (
    <>
      <View flex center>
        <Text blue40 text20L marginB-s10>
          User login
        </Text>
        <View marginB-s4 width={320}>
          <Incubator.TextField
            validateOnChange
            enableErrors
            autoCapitalize="none"
            label="Email"
            hint="Your account's email"
            value={email}
            validate={["required", "email"]}
            validationMessage={["Email is required", "Invalid email"]}
            fieldStyle={styles.withUnderline}
            onChangeText={setEmail}
            onChangeValidity={setEmailValid}
          />
          <View marginT-s5>
            <PasswordInput
              showPassword={showPassword}
              password={password}
              setPassword={setPassword}
              setPasswordValid={setPasswordValid}
              setShowPassword={setShowPassword}
            />
          </View>
        </View>
        <GoogleSigninButton size={1} onPress={loginGoogle} />
        <Button
          bg-blue50
          marginT-s2
          grey20
          text70BO
          bg-white
          label="Sign in"
          style={{
            shadowColor: Colors.grey30,
            borderRadius: 0,
            shadowOpacity: 0.6,
            shadowOffset: {width: 0, height: 2},
            shadowRadius: 2
          }}
          onPress={loginEmail}
        />
        <View marginT-s2 style={{opacity: error ? 100 : 0}}>
          <Text center text70L red10 style={styles.error}>
            {error}
          </Text>
        </View>
      </View>
      <View center row marginB-s10>
        <Text margin-s2 grey20>
          Not registered yet? Do it now!
        </Text>
        <Button bg-grey50 black label="Register" onPress={navigateToRegistration} />
      </View>
    </>
  )
}

const styles = StyleSheet.create({
  withUnderline: {
    borderBottomWidth: 1,
    borderColor: Colors.blue60,
    paddingBottom: 4
  },
  error: {position: "absolute", alignSelf: "center", width: "85%"}
})
