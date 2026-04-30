package com.example.studenteventfinder

data class Event(
    val eventId: Int,
    val title: String,
    val description: String,
    val category: String,
    val date: String,
    val time: String,
    val location: String,
    val imageUrl: String,
    val organizer: String
)