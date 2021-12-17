import React, {useEffect, useState} from "react"
import {Navigation} from "react-native-navigation"
import {Button, Colors, Image, LoaderScreen, Text, TextField, View} from "react-native-ui-lib"
import {getRestaurantById, updateRestaurant as updateRestaurantCall} from "../../../api"
import {Restaurant} from "../../../api/interfaces"
import {useLocation} from "../../../hooks/use-location"
import {useRestaurant} from "../../../hooks/use-restaurant"
import {setHomeRoot} from "../../../services/navigation"

export const Profile = () => {
  const {restaurantId} = useRestaurant()
  const [restaurant, setRestaurant] = useState<Restaurant>({} as Restaurant)
  const [updatedRestaurant, setUpdatedRestaurant] = useState<Restaurant>(restaurant)
  const [loading, setLoading] = useState(true)
  const [isSaveButtonDisabled, setIsSaveButtonDisabled] = useState(false)
  const {location} = useLocation()

  useEffect(() => {
    fetchRestaurantInfo()

    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "LOG_OUT") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  const fetchRestaurantInfo = async () => {
    const response = await getRestaurantById({
      idRestaurant: restaurantId,
      coordinates: {latitude: location.latitude, longitude: location.longitude}
    })

    if (response !== null) {
      setRestaurant(response)
      setUpdatedRestaurant(response)
    }

    setLoading(false)
  }

  const updateRestaurant = async () => {
    setIsSaveButtonDisabled(true)
    await updateRestaurantCall(updatedRestaurant)
    setIsSaveButtonDisabled(false)
  }

  const onChangeName = (name: string) => {
    setUpdatedRestaurant({
      ...updatedRestaurant,
      name
    })
  }

  const onChangeAddress = (address: string) => {
    setUpdatedRestaurant({
      ...updatedRestaurant,
      address
    })
  }

  const cancelUpdate = () => {
    setUpdatedRestaurant(restaurant)
  }

  return (
    <View flex margin-s3>
      {loading ? (
        <View flex centerH>
          <LoaderScreen color={Colors.blue30} message="Loading..." />
        </View>
      ) : (
        <View flex margin-s3>
          <View flex center>
            <Text center text40M marginT-10>
              My profile
            </Text>
            <Image
              marginT-s2
              source={{
                uri: restaurant.imageURL,
                height: 200,
                width: 330
              }}
            />
          </View>
          <View marginH-s4 flex centerV marginT-s6>
            <TextField textFieldCommonValues title="Name" value={updatedRestaurant.name} onChangeText={onChangeName} />
            <TextField
              textFieldCommonValues
              title="Location"
              value={updatedRestaurant.address}
              onChangeText={onChangeAddress}
            />
          </View>
          <Button outline label="Change Password" />
          <View
            flex
            style={{
              flexDirection: "row",
              alignItems: "center",
              justifyContent: "space-evenly"
            }}
          >
            <Button link label="Reset" onPress={cancelUpdate} />
            <Button label="Save" disabled={isSaveButtonDisabled} onPress={updateRestaurant} />
          </View>
        </View>
      )}
    </View>
  )
}
