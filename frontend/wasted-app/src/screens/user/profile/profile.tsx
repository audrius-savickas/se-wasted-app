import React, {useEffect, useState} from "react"
import {Alert, ScrollView, StyleSheet} from "react-native"
import {Navigation} from "react-native-navigation"
import {Avatar, Button, Card, Colors, Image, Text, View} from "react-native-ui-lib"
import {updateCustomerPassword} from "../../../api/customer"
import {PasswordInput} from "../../../components/password-input"
import {CustomerProfileProps} from "./interfaces"

export const Profile = ({componentId, customer}: CustomerProfileProps) => {
  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [error, setError] = useState("")

  const [showPassword, setShowPassword] = useState(false)
  const [showConfirmPassword, setShowConfirmPassword] = useState(false)

  const [saveEnabled, setSaveEnabled] = useState(false)

  const changePassword = async () => {
    if (password === confirmPassword) {
      setError("")
      const response = await updateCustomerPassword({credentials: {email: customer.mail, password}})
      if (response) {
        Alert.alert("Password changed successfully!", "You may login with your new password now.", [{text: "OK"}])
      }
    } else {
      setError("The passwords don't match.")
      setTimeout(() => {
        setError("")
      }, 3000)
    }
  }

  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "DISMISS") {
        Navigation.dismissModal(componentId)
      }
    })
    return () => listener.remove()
  }, [])

  useEffect(() => {
    if (password && confirmPassword) {
      setSaveEnabled(true)
    } else {
      setSaveEnabled(false)
    }
  }, [password, confirmPassword])

  return (
    <ScrollView>
      <View flex marginT-s10 marginH-s8>
        <View marginT-s2 center>
          <Avatar size={100} backgroundColor={Colors.purple40} name={`${customer.firstName} ${customer.lastName}`} />
          <Text marginT-s2 text60M>{`${customer.firstName} ${customer.lastName}`}</Text>
        </View>
        <View marginT-s8>
          <Text text60L>Contact Info</Text>
          <View bg-white row centerV marginT-s2 paddingV-s2 paddingH-s6 style={styles.contact}>
            <Image source={require("../../../../assets/mail-30x30.png")} />
            <Text marginL-s4 text70R>{`Email: `}</Text>
            <Text text70L>{`${customer.mail}`}</Text>
          </View>
          <View bg-white row centerV marginT-s2 paddingV-s2 paddingH-s6 style={styles.contact}>
            <Image source={require("../../../../assets/phone-30x30.png")} />
            <Text marginL-s4 text70R>{`Phone number: `}</Text>
            <Text text70L>{`${customer.phone}`}</Text>
          </View>
        </View>
        <View br40 marginT-s10>
          <Text marginB-s4 text60L>
            Change your password
          </Text>
          <View row centerV>
            <View flex centerV>
              <PasswordInput
                label="New password*"
                password={password}
                setPassword={setPassword}
                showPassword={showPassword}
                setShowPassword={setShowPassword}
                setPasswordValid={() => {}}
              />
              <View>
                <PasswordInput
                  label="Confirm new password*"
                  hint="Your account's repeated password"
                  password={confirmPassword}
                  setPassword={setConfirmPassword}
                  showPassword={showConfirmPassword}
                  setShowPassword={setShowConfirmPassword}
                  setPasswordValid={() => {}}
                />
              </View>
            </View>
          </View>
          <Card padding-s3 backgroundColor={Colors.grey70}>
            <Text text70L>Password should contain:</Text>
            <Text
              text80L
            >{`  ∙ at least 8 characters\n  ∙ 1 or more capital letters\n  ∙ 1 digit\n  ∙ 1 special character`}</Text>
          </Card>
          <Button
            marginT-s2
            bg-purple40
            label="Save password"
            style={styles.button}
            disabled={!saveEnabled}
            onPress={changePassword}
          />
          <View marginT-s2 style={{opacity: error ? 100 : 0}}>
            <Text center text70L red10 style={styles.error}>
              {error}
            </Text>
          </View>
        </View>
      </View>
    </ScrollView>
  )
}

const styles = StyleSheet.create({
  contact: {
    borderColor: Colors.grey40,
    borderWidth: 1,
    borderRadius: 20,
    shadowColor: Colors.black,
    shadowRadius: 2,
    shadowOffset: {width: 0, height: 0},
    shadowOpacity: 0.3
  },
  button: {
    width: 150,
    alignSelf: "center"
  },
  error: {position: "absolute", alignSelf: "center", width: "85%"}
})
