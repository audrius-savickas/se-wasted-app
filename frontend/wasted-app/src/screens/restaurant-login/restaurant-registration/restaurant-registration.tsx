import React, {useEffect, useState} from "react"
import {Alert, ScrollView, StyleSheet} from "react-native"
import {Assets, Button, Card, Colors, Incubator, Text, TextField, View} from "react-native-ui-lib"
import {registerRestaurant} from "../../../api"
import {PasswordInput} from "../../../components/password-input"
import {convertPassword} from "../../../utils/credentials"
import {RestaurantRegistrationProps} from "./interfaces"

export const RestaurantRegistration = ({componentId}: RestaurantRegistrationProps) => {
  const [name, setName] = useState("")
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [address, setAddress] = useState("")
  const [imageUrl, setImageUrl] = useState("")

  const [showPassword, setShowPassword] = useState(false)
  const [showConfirmPassword, setShowConfirmPassword] = useState(false)

  const [nameValid, setNameValid] = useState(true)
  const [emailValid, setEmailValid] = useState(true)
  const [passwordValid, setPasswordValid] = useState(true)
  const [confirmPasswordValid, setConfirmPasswordValid] = useState(true)
  const [locationValid, setAddressValid] = useState(true)
  const [imageUrlValid, setImageUrlValid] = useState(true)

  const [error, setError] = useState("")

  const valid = nameValid && emailValid && passwordValid && confirmPasswordValid && locationValid && imageUrlValid

  const finishRegistration = async () => {
    if (valid) {
      if (password !== confirmPassword) {
        setError("Passwords don't match")
      } else {
        const restaurantId = await registerRestaurant({
          name,
          coords: {latitude: 10, longitude: 10},
          credentials: {email, password},
          address,
          imageUrl
        })
        if (!restaurantId) {
          setError("There is already an account registered on this email.")
        } else {
          Alert.alert("Registered succesfully!", "Please check your inbox for confirmation email.", [{text: "OK"}])
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
    <>
      <ScrollView>
        <View flexG center marginB-s10 marginT-s8>
          <View br30 margin-s4 marginB-s8 bg-grey70 padding-s4>
            <Text text70L>Please fill these fields in order to register your restaurant!{`\n* - required fields`}</Text>
          </View>
          <View centerV width={320}>
            <Incubator.TextField
              validateOnChange
              enableErrors
              marginB-s2
              autoCapitalize="none"
              hint="Your restaurant's name"
              fieldStyle={styles.withUnderline}
              label="Restaurant Name*"
              validate={["required"]}
              validationMessage="Name is required"
              value={name}
              onChangeText={setName}
              onChangeValidity={setNameValid}
            />
            <Incubator.TextField
              validateOnChange
              enableErrors
              marginB-s6
              autoCapitalize="none"
              hint="Your restaurant's email*"
              fieldStyle={styles.withUnderline}
              label="Email"
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
            <Incubator.TextField
              marginB-s2
              marginT-s4
              validateOnChange
              enableErrors
              autoCapitalize="none"
              fieldStyle={styles.withUnderline}
              label="Address*"
              hint="Your restaurant's address"
              value={address}
              validate={["required"]}
              validationMessage="Address is required"
              onChangeText={setAddress}
              onChangeValidity={setAddressValid}
            />
            {/* TODO: implement location picking */}
            <Incubator.TextField
              marginB-s6
              validateOnChange
              enableErrors
              autoCapitalize="none"
              fieldStyle={styles.withUnderline}
              label="Image URL*"
              hint="Your restaurant's image's URL"
              value={imageUrl}
              validate={["required"]}
              validationMessage="Image URL is required"
              onChangeText={setImageUrl}
              onChangeValidity={setImageUrlValid}
            />
          </View>
          <Button bg-blue40 label="Register" onPress={finishRegistration} />
          <View marginT-s2 style={{opacity: error ? 100 : 0}}>
            <Text center text70L red10 style={styles.error}>
              {error}
            </Text>
          </View>
        </View>
      </ScrollView>
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
