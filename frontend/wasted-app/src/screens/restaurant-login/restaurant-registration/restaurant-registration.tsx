import React, {useEffect, useState} from "react"
import {Alert, ScrollView, StyleSheet} from "react-native"
import Geocoder from "react-native-geocoding"
import {Navigation} from "react-native-navigation"
import {Assets, Button, Card, Colors, Image, Incubator, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {registerRestaurant} from "../../../api"
import {Coordinates} from "../../../api/interfaces"
import {Map} from "../../../components/map"
import {PasswordInput} from "../../../components/password-input"
import {RestaurantRegistrationProps} from "./interfaces"

export const RestaurantRegistration = ({componentId}: RestaurantRegistrationProps) => {
  const [name, setName] = useState("")
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [address, setAddress] = useState("")
  const [imageUrl, setImageUrl] = useState("")
  const [coordinates, setCoordinates] = useState({
    latitude: 54.687157,
    longitude: 25.279652
  } as Coordinates)
  const [coordinatesDelta, setCoordinatesDelta] = useState({latitudeDelta: 0.0922, longitudeDelta: 0.0421})
  const [description, setDescription] = useState("")

  const [coordinatesLoading, setCoordinatesLoading] = useState(false)

  const [showPassword, setShowPassword] = useState(false)
  const [showConfirmPassword, setShowConfirmPassword] = useState(false)

  const [nameValid, setNameValid] = useState(true)
  const [emailValid, setEmailValid] = useState(true)
  const [passwordValid, setPasswordValid] = useState(true)
  const [confirmPasswordValid, setConfirmPasswordValid] = useState(true)
  const [addressValid, setAddressValid] = useState(true)

  const [error, setError] = useState("")

  const valid =
    nameValid &&
    name &&
    emailValid &&
    email &&
    passwordValid &&
    password &&
    confirmPasswordValid &&
    confirmPassword &&
    addressValid &&
    !coordinatesLoading

  const finishRegistration = async () => {
    if (valid) {
      if (password !== confirmPassword) {
        setError("Passwords don't match")
      } else {
        const restaurantId = await registerRestaurant({
          name,
          coords: {latitude: coordinates.latitude, longitude: coordinates.longitude},
          credentials: {email, password},
          address,
          imageUrl,
          description
        })
        if (!restaurantId) {
          setError("There is already an account registered on this email.")
        } else {
          setError("")
          Alert.alert("Registered succesfully!", "Please check your inbox for confirmation email.", [{text: "OK"}])
          Navigation.pop(componentId)
        }
      }
    } else {
      setError("Please check your input fields.")
    }
  }

  const fetchCoordinates = async () => {
    setCoordinatesLoading(true)
    const response = await Geocoder.from(address)
    const coords = response.results[0].geometry.location
    setAddress(response.results[0].formatted_address)
    setCoordinates({latitude: coords.lat, longitude: coords.lng})
    setCoordinatesDelta({latitudeDelta: 0.003, longitudeDelta: 0.003})
    setCoordinatesLoading(false)
  }

  useEffect(() => {
    if (valid) {
      setError("")
    }
  }, [valid])

  useEffect(() => {
    Navigation.mergeOptions(componentId, {topBar: {rightButtons: [{text: "Done", id: "DONE"}]}})

    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "DONE") {
        finishRegistration()
      }
    })
    return () => listener.remove()
  }, [])

  return (
    <>
      <ScrollView>
        <View flexG center marginB-s10 marginT-s8>
          <View br30 margin-s4 marginB-s8 bg-grey70 padding-s4>
            <Text text70L>Please fill these fields in order to register your restaurant!{`\n* - required fields`}</Text>
          </View>
          <View centerV width={340}>
            <Incubator.TextField
              validateOnChange
              enableErrors
              marginB-s2
              autoCapitalize="none"
              hint="Your restaurant's name"
              fieldStyle={styles.withUnderline}
              label="Restaurant Name*"
              validate="required"
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
              hint="Your restaurant's email"
              fieldStyle={styles.withUnderline}
              label="Email*"
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
            <View marginB-s6 marginT-s4>
              <Incubator.TextField
                validateOnChange
                enableErrors
                autoCapitalize="none"
                fieldStyle={styles.withUnderline}
                label="Address*"
                hint="Your restaurant's address"
                value={address}
                validate={["required"]}
                validationMessage="Address is required"
                onBlur={fetchCoordinates}
                onChangeText={setAddress}
                onChangeValidity={setAddressValid}
              />
              <TouchableOpacity
                style={{position: "absolute", alignSelf: "flex-end", top: 15}}
                onPress={fetchCoordinates}
              >
                <Image source={Assets.icons.search} />
              </TouchableOpacity>
              <View marginT-s1>
                <Map style={styles.map} coordinates={coordinates} coordinatesDelta={coordinatesDelta} />
              </View>
            </View>
            <Incubator.TextField
              marginB-s2
              validateOnChange
              enableErrors
              autoCapitalize="none"
              fieldStyle={styles.withUnderline}
              label="Image URL (optional)"
              hint="Your restaurant's image's URL"
              value={imageUrl}
              onChangeText={setImageUrl}
            />
            <Incubator.TextField
              marginB-s6
              paddingT-s2
              paddingH-s2
              multiline
              showCharCounter
              maxLength={200}
              label="Description (optional)"
              fieldStyle={{borderColor: Colors.blue60, borderWidth: 1, height: 100}}
              onChangeText={setDescription}
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
  error: {position: "absolute", alignSelf: "center", width: "85%"},
  map: {
    height: 200
  }
})
