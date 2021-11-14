import React, {useEffect, useState} from "react"
import {StyleSheet} from "react-native"
import {Button, Colors, Incubator, Text, View} from "react-native-ui-lib"
import {loginRestaurant} from "../../api"
import {navigateToRestaurantRegistration, setRestaurantRoot} from "../../services/navigation"
import {RestaurantLoginProps} from "./interfaces"

export const RestaurantLogin = ({componentId}: RestaurantLoginProps) => {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const [emailValid, setEmailValid] = useState(true)
  const [passwordValid, setPasswordValid] = useState(true)
  const [error, setError] = useState("")

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
          <Incubator.TextField
            marginT-s5
            validateOnChange
            enableErrors
            secureTextEntry
            label="Password"
            autoCapitalize="none"
            hint="Your account's password"
            value={password}
            validate={"required"}
            validationMessage="Password is required"
            fieldStyle={styles.withUnderline}
            onChangeText={setPassword}
            onChangeValidity={setPasswordValid}
          />
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
