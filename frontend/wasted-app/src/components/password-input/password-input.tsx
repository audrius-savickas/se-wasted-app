import React from "react"
import {StyleSheet, View} from "react-native"
import {Colors, Image, Incubator, TouchableOpacity} from "react-native-ui-lib"
import {PasswordInputProps} from "./interfaces"

export const PasswordInput = ({
  label = "Password",
  hint = "Your account's password",
  showPassword,
  password,
  setPassword,
  setPasswordValid,
  setShowPassword
}: PasswordInputProps) => {
  return (
    <View>
      <Incubator.TextField
        flexS
        validateOnChange
        enableErrors
        secureTextEntry={!showPassword}
        label={label}
        autoCapitalize="none"
        hint={hint}
        value={password}
        validate={"required"}
        validationMessage="Password is required"
        fieldStyle={styles.withUnderline}
        onChangeText={setPassword}
        onChangeValidity={setPasswordValid}
      />
      <TouchableOpacity
        style={{position: "absolute", right: 0, alignSelf: "flex-start"}}
        onPressIn={() => setShowPassword(true)}
        onPressOut={() => setShowPassword(false)}
      >
        <Image
          source={require("../../../assets/view.png")}
          style={{width: 28, height: 28, position: "absolute", right: 0, top: 10}}
        />
      </TouchableOpacity>
    </View>
  )
}

const styles = StyleSheet.create({
  withUnderline: {
    borderBottomWidth: 1,
    borderColor: Colors.blue60,
    paddingBottom: 4
  }
})
