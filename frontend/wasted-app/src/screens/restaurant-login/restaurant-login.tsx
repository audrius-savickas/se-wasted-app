import {GoogleSignin, GoogleSigninButton, statusCodes, User} from "@react-native-google-signin/google-signin"
import React, {useEffect, useState} from "react"
import {StyleSheet} from "react-native"
import {Button, Colors, Incubator, Text, View} from "react-native-ui-lib"
import {loginRestaurant} from "../../api"
import {PasswordInput} from "../../components/password-input"
import {navigateToRestaurantRegistration, setRestaurantRoot} from "../../services/navigation"
import {RestaurantLoginProps} from "./interfaces"

GoogleSignin.configure({iosClientId: "834850407777-uv37m0m83itkc63p628t4hs52vabfrnh.apps.googleusercontent.com"})
// GoogleSignin.configure({iosClientId: "com.googleusercontent.apps.834850407777-uv37m0m83itkc63p628t4hs52vabfrnh"})

export const RestaurantLogin = ({componentId}: RestaurantLoginProps) => {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [userInfo, setUserInfo] = useState({} as User)

  const [emailValid, setEmailValid] = useState(true)
  const [passwordValid, setPasswordValid] = useState(true)
  const [error, setError] = useState("")
  const [showPassword, setShowPassword] = useState(false)

  const valid = passwordValid && emailValid

  const login = async () => {
    const restaurantId = await loginRestaurant({email, password})
    if (valid) {
      if (restaurantId) {
        setRestaurantRoot({restaurantId})
        setError("")
      } else {
        setError("Login failed. We haven't found a registered account with these credentials.")
      }
    } else {
      setError("Please check your input fields.")
    }
  }

  const signIn = async () => {
    try {
      await GoogleSignin.hasPlayServices()
      const userInfo = await GoogleSignin.signIn()
      console.log(userInfo)
      setUserInfo(userInfo)
    } catch (error) {
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

  const navigateToRegistration = () => {
    navigateToRestaurantRegistration(componentId, {})
  }

  useEffect(() => {
    if (valid) {
      setError("")
    }
  }, [valid])

  return (
    <>
      <View flexG center marginT-s4>
        <Text blue40 text20L marginB-s10>
          Restaurant login
        </Text>
        <GoogleSigninButton onPress={signIn} />
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
        <Button bg-blue50 black label="Login" onPress={login} />
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
