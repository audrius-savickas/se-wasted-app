import React, {useEffect, useState} from "react"
import {Alert, LogBox, ScrollView, StyleSheet} from "react-native"
import Geocoder from "react-native-geocoding"
import {GooglePlacesAutocomplete} from "react-native-google-places-autocomplete"
import {Navigation} from "react-native-navigation"
import {Button, Card, Colors, Image, Incubator, Text, View} from "react-native-ui-lib"
import {GOOGLE_MAPS_API_KEY} from "../../../../credentials"
import {registerRestaurant} from "../../../api"
import {Coordinates} from "../../../api/interfaces"
import {Map} from "../../../components/map"
import {PasswordInput} from "../../../components/password-input"
import {RestaurantRegistrationProps} from "./interfaces"

LogBox.ignoreLogs(["VirtualizedLists should never be nested"])

export const RestaurantRegistration = ({componentId}: RestaurantRegistrationProps) => {
  const [name, setName] = useState("")
  const [email, setEmail] = useState("")
  const [phone, setPhone] = useState("")
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
  const [phoneValid, setPhoneValid] = useState(true)
  const [passwordValid, setPasswordValid] = useState(true)
  const [confirmPasswordValid, setConfirmPasswordValid] = useState(true)
  const [addressValid, setAddressValid] = useState(true)

  const [error, setError] = useState("")

  const [loadImage, setLoadImage] = useState(false)

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
    phoneValid &&
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
          description,
          phone
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

  const onAddressChange = async (data: any, details: any) => {
    setAddress(details.formatted_address)
    setAddressValid(true)
  }

  useEffect(() => {
    if (address) {
      fetchCoordinates()
    }
  }, [address])

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
    <ScrollView keyboardShouldPersistTaps="always">
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
            autoCapitalize="none"
            hint="Your restaurant's email"
            fieldStyle={styles.withUnderline}
            label="Phone number*"
            validate={["required"]}
            validationMessage={["Phone number is required"]}
            value={phone}
            onChangeText={setPhone}
            onChangeValidity={setPhoneValid}
          />
          <Incubator.TextField
            validateOnChange
            enableErrors
            marginB-s6
            autoCapitalize="none"
            hint="Your restaurant's email"
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
          <View marginB-s6 marginT-s4>
            <Text grey10>Address*</Text>
            <GooglePlacesAutocomplete
              suppressDefaultStyles
              fetchDetails
              placeholder="Your restaurant's address"
              query={{key: GOOGLE_MAPS_API_KEY}}
              styles={{
                textInput: {...styles.withUnderline, padding: 0, margin: 0},
                container: {padding: 0, marginTop: 6, marginBottom: 0},
                row: {
                  backgroundColor: "#FFFFFF",
                  padding: 13,
                  height: 44,
                  flexDirection: "row"
                },
                separator: {
                  height: 0.5,
                  backgroundColor: "#c8c7cc"
                }
              }}
              textInputProps={{onBlur: () => !address && setAddressValid(false)}}
              onPress={onAddressChange}
            />
            <Text red30 style={{opacity: addressValid ? 0 : 100}}>
              Address is required
            </Text>
            <View marginT-s1>
              <Map style={styles.map} coordinates={coordinates} coordinatesDelta={coordinatesDelta} />
            </View>
          </View>
          <Incubator.TextField
            validateOnChange
            enableErrors
            autoCapitalize="none"
            fieldStyle={styles.withUnderline}
            label="Image URL (optional)"
            hint="Your restaurant's image's URL"
            value={imageUrl}
            onChangeText={(text: string) => {
              setLoadImage(false)
              setImageUrl(text)
            }}
            onBlur={() => setLoadImage(true)}
          />
          {loadImage && (
            <View style={styles.shadow}>
              <Image
                source={{uri: imageUrl, height: 200}}
                style={{
                  height: 200,
                  width: "100%"
                }}
              />
            </View>
          )}
          <Incubator.TextField
            marginB-s6
            marginT-s2
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
  },
  shadow: {
    shadowColor: Colors.black,
    shadowOffset: {width: 0, height: 0},
    shadowRadius: 3,
    shadowOpacity: 0.7
  }
})
