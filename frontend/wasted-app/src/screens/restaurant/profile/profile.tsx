import React, {useEffect, useState} from "react"
import {Alert, ScrollView, StyleSheet} from "react-native"
import Geocoder from "react-native-geocoding"
import {GooglePlacesAutocomplete} from "react-native-google-places-autocomplete"
import {Navigation} from "react-native-navigation"
import {Avatar, Button, Card, Colors, Image, Incubator, Text, View} from "react-native-ui-lib"
import {GOOGLE_MAPS_API_KEY} from "../../../../credentials"
import {updateRestaurantApi, updateRestaurantPassword} from "../../../api"
import {Coordinates} from "../../../api/interfaces"
import {Map} from "../../../components/map"
import {PasswordInput} from "../../../components/password-input"
import {useRestaurant} from "../../../hooks/use-restaurant"
import {RestaurantProfileProps} from "./interfaces"

export const Profile = ({componentId}: RestaurantProfileProps) => {
  const {restaurant, updateRestaurant} = useRestaurant()
  const [error, setError] = useState("")

  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [showPassword, setShowPassword] = useState(false)
  const [showConfirmPassword, setShowConfirmPassword] = useState(false)
  const [passwordSaveEnabled, setPasswordSaveEnabled] = useState(false)

  const [imageUrl, setImageUrl] = useState("")
  const [description, setDescription] = useState("")
  const [coordinates, setCoordinates] = useState({
    latitude: 54.687157,
    longitude: 25.279652
  } as Coordinates)
  const [coordinatesDelta, setCoordinatesDelta] = useState({latitudeDelta: 0.0922, longitudeDelta: 0.0421})
  const [address, setAddress] = useState("")
  const [addressInput, setAddressInput] = useState("")
  const [infoSaveEnabled, setInfoSaveEnabled] = useState(false)

  const [addressValid, setAddressValid] = useState(true)

  const changePassword = async () => {
    if (password === confirmPassword) {
      setError("")
      const response = await updateRestaurantPassword({credentials: {email: "umarasss@gmail.com", password}}) //TODO: get email with restaurant
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

  const fetchCoordinates = async () => {
    const response = await Geocoder.from(address)
    const coords = response.results[0].geometry.location
    setCoordinates({latitude: coords.lat, longitude: coords.lng})
    setCoordinatesDelta({latitudeDelta: 0.003, longitudeDelta: 0.003})
  }

  const onAddressChange = async (data: any, details: any) => {
    setAddress(details.formatted_address)
    setAddressInput(details.formatted_address)
    setAddressValid(true)
  }

  const changeInfo = async () => {
    const resp = await updateRestaurantApi({
      ...restaurant,
      address,
      imageURL: imageUrl,
      coords: coordinates,
      description
    })
    if (resp) {
      Alert.alert(
        "Your information has been updated successfully!",
        "It will take a few minutes for the information to update",
        [{text: "OK"}]
      )
      updateRestaurant({address, imageURL: imageUrl, coords: coordinates, description})
      setInfoSaveEnabled(false)
    } else {
      console.error("Update failed")
    }
  }

  useEffect(() => {
    if (address) {
      fetchCoordinates()
    }
  }, [address])

  useEffect(() => {
    if (Object.keys(restaurant).length) {
      setImageUrl(restaurant.imageURL)
      setDescription(restaurant.description)
      setCoordinates(restaurant.coords)
      setCoordinatesDelta({latitudeDelta: 0.003, longitudeDelta: 0.003})
      setAddressInput(restaurant.address)
      setAddress(restaurant.address)
    }
  }, [restaurant])

  useEffect(() => {
    if (password && confirmPassword) {
      setPasswordSaveEnabled(true)
    } else {
      setPasswordSaveEnabled(false)
    }
  }, [password, confirmPassword])

  useEffect(() => {
    if (
      restaurant.imageURL !== imageUrl ||
      restaurant.address !== addressInput ||
      restaurant.description !== description
    ) {
      setInfoSaveEnabled(true)
    } else {
      setInfoSaveEnabled(false)
    }
  }, [address, imageUrl, description, restaurant])

  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "DISMISS") {
        Navigation.dismissModal(componentId)
      }
    })
    return () => listener.remove()
  }, [])

  return (
    <ScrollView keyboardShouldPersistTaps="always">
      <View flex marginT-s10 marginH-s8>
        <View marginT-s2 center>
          <Avatar
            size={100}
            backgroundColor={Colors.purple40}
            name={restaurant.name}
            source={{uri: restaurant.imageURL}}
          />
          <Text marginT-s2 text60M>
            {restaurant.name}
          </Text>
        </View>
        <View marginT-s8>
          <Text text60L>Contact Info</Text>
          <View bg-white row centerV marginT-s2 paddingV-s2 paddingH-s6 style={styles.contact}>
            <Image source={require("../../../../assets/mail-30x30.png")} />
            <Text marginL-s4 text70R>{`Email: `}</Text>
            <Text text70L>{`${restaurant.mail}`}</Text>
          </View>
          <View bg-white row centerV marginT-s2 paddingV-s2 paddingH-s6 style={styles.contact}>
            <Image source={require("../../../../assets/phone-30x30.png")} />
            <Text marginL-s4 text70R>{`Phone number: `}</Text>
            <Text text70L>{`${restaurant.phone}`}</Text>
          </View>
        </View>
        <View marginT-s10>
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
            disabled={!passwordSaveEnabled}
            onPress={changePassword}
          />
          <View marginT-s2 style={{opacity: error ? 100 : 0}}>
            <Text center text70L red10 style={styles.error}>
              {error}
            </Text>
          </View>
        </View>
        <View marginT-s6>
          <Text marginB-s4 text60L>
            Change your info
          </Text>
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
              textInputProps={{
                onBlur: () => !address && setAddressValid(false),
                value: addressInput,
                onChangeText: text => setAddressInput(text)
              }}
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
            paddingT-s2
            paddingH-s2
            multiline
            showCharCounter
            maxLength={200}
            label="Description (optional)"
            fieldStyle={{borderColor: Colors.blue60, borderWidth: 1, height: 100}}
            style={{height: "100%"}}
            value={description}
            onChangeText={setDescription}
          />
          <Button
            bg-purple40
            label="Save info"
            style={styles.button}
            disabled={!infoSaveEnabled}
            onPress={changeInfo}
          />
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
  error: {position: "absolute", alignSelf: "center", width: "85%"},
  withUnderline: {
    borderBottomWidth: 1,
    borderColor: Colors.blue60,
    paddingBottom: 4
  },
  map: {
    height: 200
  }
})
