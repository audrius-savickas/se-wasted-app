import React from "react"
import {StyleSheet, View} from "react-native"
import {Colors, Image, Incubator, TouchableOpacity} from "react-native-ui-lib"
import {PasswordInputProps} from "./interfaces"

export const PasswordInput = ({
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
        marginT-s5
        validateOnChange
        enableErrors
        secureTextEntry={!showPassword}
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
      <TouchableOpacity
        style={{position: "absolute", left: 300, right: 0}}
        onPressIn={() => setShowPassword(true)}
        onPressOut={() => setShowPassword(false)}
      >
        <Image source={require("../../../assets/view.png")} style={{top: 30, right: 7, width: 28, height: 28}} />
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
