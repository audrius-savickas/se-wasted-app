import {GoogleSigninButton} from "@react-native-google-signin/google-signin"
import React, {useState} from "react"
import {StyleSheet} from "react-native"
import {Button, Colors, Incubator, Text, View} from "react-native-ui-lib"
import {PasswordInput} from "../../components/password-input"
import {UserLoginProps} from "./interfaces"

export const UserLogin = ({componentId}: UserLoginProps) => {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const [emailValid, setEmailValid] = useState(true)
  const [passwordValid, setPasswordValid] = useState(true)
  const [error, setError] = useState("")
  const [showPassword, setShowPassword] = useState(false)

  const loginGoogle = () => {}

  const loginEmail = () => {}

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
          label="Sign in with email"
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
        <Button bg-grey50 black label="Register" onPress={() => {}} />
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
